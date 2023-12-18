using Data.Context;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface ISchedulerTaskService
    {
        Task QueueTask(SchedulerTaskItem item);
        Task RemoveTask(string jobId);
    }
}
