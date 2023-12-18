using Data.Context;
using Data.Entity;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SchedulerTaskService : ISchedulerTaskService
    {
        private readonly ILogger<SchedulerTaskService> _logger;
        private readonly SUBDbContext _context;

        public SchedulerTaskService(ILogger<SchedulerTaskService> logger, SUBDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task QueueTask(SchedulerTaskItem item)
        {
            _logChange(item, "Scheduled Item");
            var jobEntity = new ScheduledJobEntity();
            jobEntity.HangFireJobId = BackgroundJob.Schedule(item.DoWork, item.ScheduledFor);
            jobEntity.AppJobId = item.JobId;
            jobEntity.ScheduledFor = item.ScheduledFor;
            _context.ScheduledJobs.Add(jobEntity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTask(string jobId)
        {
            var job = await _context.ScheduledJobs
                .Where(j => j.AppJobId == jobId)
                .FirstOrDefaultAsync();
            if (job != null)
            {
                BackgroundJob.Delete(job.HangFireJobId);
                _context.ScheduledJobs.Remove(job);
                await _context.SaveChangesAsync();
            }
        }

        private void _logChange(SchedulerTaskItem schedule, string additionalText)
        {
            var text = $"SCHEDULER: ID - {schedule.JobId} Time: '{schedule.ScheduledFor.UtcDateTime}' ({schedule.Type.ToString()})";
            _logger.LogInformation($"{text} - {additionalText}");
        }
    }
}
