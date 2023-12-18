using Common.Singletons;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class TripBackgroundTasks : ITripBackgroundTasks
    {
        private readonly IServiceProvider _services;

        public TripBackgroundTasks(IServiceProvider services)
        {
            _services = services;
        }

        public async Task EndOfOrderAsync(int tripId)
        {
            using (var scope = _services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SUBDbContext>();
                var schedulerTaskService = scope.ServiceProvider.GetRequiredService<ISchedulerTaskService>();
                var appCache = scope.ServiceProvider.GetRequiredService<IAppCache>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var tripBackgroundTasks = scope.ServiceProvider.GetRequiredService<ITripBackgroundTasks>();

                var appConfig = appCache.ConfigCache;
                var trip = await context.Trip
                    .Include(trip => trip.Offers)
                    .Where(trip => trip.Id == tripId)
                    .FirstOrDefaultAsync();

                trip.IsDone = true;
                if (trip.Offers.Count > 0)
                {
                    //
                }
                else
                {
                    await emailService.SendTripDoneNoOffers(trip.Id, (int)trip.OwnerId);
                }
                await context.SaveChangesAsync();
            }
        }

    }
}
