using Common.Enums;
using GoogleApi.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entity
{
    public class EmailConfigEntity
    {
        public int Id { get; set; }
        public EmailType Type { get; set; }
        public Language Language { get; set; }
        public int TemplateId { get; set; }

        public EmailConfigEntity()
        { }
    }
}
