using Common.Enums;
using GoogleApi.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UpdateEmailConfig
    {
        [Required]
        [Display(Name = "type")]
        public EmailType Type { get; set; }

        [Required]
        [Display(Name = "language")]
        public Language Language { get; set; }

        [Required]
        [Display(Name = "templateId")]
        public int TemplateId { get; set; }
    }
}
