using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entity
{
    public class ScheduledJobEntity
    {
        [Key]
        public string HangFireJobId { get; set; }

        public string AppJobId { get; set; }

        public DateTimeOffset ScheduledFor { get; set; }
    }
}
