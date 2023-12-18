using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using Common.Singletons;
using Data.Context;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TripService : ITripService
    {
        private readonly SUBDbContext _context;
        private readonly ISchedulerTaskService _schedulerTaskService;
        private readonly ITripBackgroundTasks _tripBackgroundTasks;
        private readonly IEmailService _emailService;
        private readonly IAppCache _appCache;

        public TripService(SUBDbContext context, ISchedulerTaskService schedulerTaskService, 
            IEmailService emailService, IAppCache appCache, ITripBackgroundTasks tripBackgroundTasks)
        {
            _context = context;
            _schedulerTaskService = schedulerTaskService;
            _tripBackgroundTasks = tripBackgroundTasks;
            _emailService = emailService;
            _appCache = appCache;
        }

        public async Task CreateTrip(CreateTripRequestDto req, CurrentUserDto currentUser)
        {
            var user = await _context.User
                .Include(user => user.Address)
                .Include(user => user.MailingAddress)
                .Where(user => user.Id == currentUser.Id).FirstOrDefaultAsync();

            var firstRoute = _validationForCreateTrip(req, user);

            var item = new TripEntity();
            item.InitEntity(req);
            item.Owner = user;
            _context.Trip.Add(item);

            await _context.SaveChangesAsync();
            await _scheduleEndOfOrderAsync(item);
            await _emailService.SendTripCreate(item.Id, user.Id);
            return;

        }

        public async Task<List<TripItemResponseDto>> GetAllTrips(TripListRequestDto req, CurrentUserDto currentUser)
        {
            var trips = await _context.Trip
                .OrderByDescending(trip => trip.Id)
                .Skip(req.Offset).Take(req.Limit)
                .Include(trip => trip.Owner)
                .Include(trip => trip.Offers)
                .Where(trip => req.DietForTransporter == null || trip.DietForTransporter == req.DietForTransporter)
                .Where(trip => req.MaxNumberOfPersons == null || trip.NumberOfPersons <= req.MaxNumberOfPersons)
                .Where(trip => req.Start == null || trip.StartTrip >= req.Start)
                .Where(trip => req.OnlyMine == null || trip.Owner.Id == currentUser.Id)
                .Where(trip => req.MeOffered == null || trip.Offers.Select(o => o.UserId).Contains(currentUser.Id))
                .Where(trip => req.DistanceFromInMeters == null || trip.TotalDistanceInMeters >= req.DistanceFromInMeters)
                .Where(trip => req.DistanceToInMeters == null || trip.TotalDistanceInMeters <= req.DistanceToInMeters)
                .ToListAsync();

            var tripIds = trips.Select(t => t.Id).ToList();

            var routes = await _context.Route
                .Where(route => tripIds.Contains((int)route.TripId))
                .Include(route => route.From).ThenInclude(s => s.Place).ThenInclude(p => p.Translations)
                .Include(route => route.To).ThenInclude(s => s.Place).ThenInclude(p => p.Translations)
                .ToListAsync();

            trips.ForEach(trip =>
            {
                trip.Routes = routes.Where(route => route.TripId == trip.Id).ToList();
            });

            var responseItems = trips.Select(t =>
            {
                var r = t.convertToItemResponse();
                r.IsMine = t.Owner.Id == currentUser.Id;
                r.HasOffers = t.Offers.Count > 0;
                return r;
            }).ToList();

            if (currentUser.Role == UserRole.TRANSPORTER)
            {
                responseItems.ForEach(trip =>
                {
                    var offers = trips
                    .Where(item => item.Id == trip.Id)
                    .SelectMany(item => item.Offers).ToList();

                    var foundOffer = offers.Where(offer => offer.UserId == currentUser.Id).FirstOrDefault();
                    trip.AlreadyOffered = foundOffer == null ? false : true;
                });
            }

            return responseItems;
        }

        private RouteRequestDto _validationForCreateTrip(CreateTripRequestDto req, UserEntity user)
        {
            var appConfig = _appCache.ConfigCache;

            if (user == null)
            {
                throw new NotFoundSUBException(ErrorCode.UNKNOWN_EMAIL, "Unknown user");
            }
            if (user.IsCompany && string.IsNullOrEmpty(user.Ico))
            {
                throw new NotFoundSUBException(ErrorCode.UNKNOWN, "ICO is required for comnpany");
            }
            if (!user.hasValidInvoiceInformation())
            {
                throw new NotFoundSUBException(ErrorCode.UNKNOWN, "Invalid user settings for create trip");
            }
            if (req.EndOrder < DateTimeOffset.UtcNow.AddHours(appConfig.MinEndOrderFromNowInHours))
            {
                throw new BadRequestSUBException(ErrorCode.UNKNOWN, "min date end of order in hours is " + appConfig.MinEndOrderFromNowInHours);
            }
            var firstRoute = req.Routes.First();
            var span = firstRoute.Start.Subtract(req.EndOrder);
            if (span.TotalHours < appConfig.MinDiffBetweenStartTripAndEndOrderInHours)
            {
                throw new BadRequestSUBException(ErrorCode.UNKNOWN, "min Diff between End date and Start trip in hours is " + appConfig.MinDiffBetweenStartTripAndEndOrderInHours);
            }
            return firstRoute;
        }

        private async Task _scheduleEndOfOrderAsync(TripEntity trip)
        {
            var item = new SchedulerTaskItem(
                SchedulerTaskItem.GenerateJobId(trip.Id.ToString(), ScheduleType.TRIP_END),
                trip.EndOrder,
                ScheduleType.TRIP_END,
                () => _tripBackgroundTasks.EndOfOrderAsync(trip.Id)
                );
            await _schedulerTaskService.QueueTask(item);
        }
    }
}
