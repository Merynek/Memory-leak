using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Services;
using Common.Attributes;
using Common.Dto;
using Common.Enums;
using Common.Singletons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sub.Api.ActionFilters;

namespace Sub.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : BaseApiController<TripController>
    {
        private readonly ITripService _tripService;
        private readonly ILocker _locker;

        public TripController(
            ITripService tripService,
            ILogger<TripController> logger,
            IHttpContextAccessor haccess,
            ILocker locker
            ) : base(logger, haccess)
        {
            _tripService = tripService;
            _locker = locker;
        }

        [HttpGet("list")]        
        public async Task<ActionResult<IEnumerable<TripItemResponseDto>>> GetAllTrips([FromQuery] TripListRequestDto req)
        {
            return await _tripService.GetAllTrips(req, _currentUser);
        }


        [HttpPost("")]
        [AuthorizeRole(UserRole.DEMANDER)]
        [TypeFilter(typeof(LogActionFilter))]
        public async Task<ActionResult> NewTrip(CreateTripRequestDto req)
        {
            var key = "USER: " + _currentUser.Id + " - CREATE TRIP";
            await _locker.ExecuteAsync(key, async () =>
            {
                await _tripService.CreateTrip(req, _currentUser);
            });            

            return Ok();
        }
    }

}
