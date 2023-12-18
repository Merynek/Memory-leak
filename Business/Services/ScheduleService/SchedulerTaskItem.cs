using Common.Enums;
using System;
using System.Linq.Expressions;

namespace Business.Services
{

    public class SchedulerTaskItem
    {
        public string JobId { get; }
        public DateTimeOffset ScheduledFor { get; }
        public ScheduleType Type { get; }
        public Expression<Action> DoWork { get; }

        public SchedulerTaskItem(string jobId, DateTimeOffset scheduledFor, ScheduleType type, Expression<Action> doWork)
        {
            JobId = jobId;
            ScheduledFor = scheduledFor;
            Type = type;
            DoWork = doWork;
        }

        public static string GenerateJobId(string jobId, ScheduleType type)
        {
            return type.ToString() + "-" + jobId;
        }
    }
}
