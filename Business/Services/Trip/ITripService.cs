using Common.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ITripService
    {

        Task CreateTrip(CreateTripRequestDto req, CurrentUserDto currentUser);

        Task<List<TripItemResponseDto>> GetAllTrips(TripListRequestDto req, CurrentUserDto currentUser);
    }
}
