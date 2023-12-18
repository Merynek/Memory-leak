using System.Threading.Tasks;

namespace Business.Services
{
    public interface ITripBackgroundTasks
    {
        Task EndOfOrderAsync(int tripId);
    }
}
