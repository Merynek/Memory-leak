using Common.Enums;
using GoogleApi.Entities.Common.Enums;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class EmailConfigResponseDto
    {
        [Required]
        public Dictionary<UserParams, string> UserParams {get; set;}

        [Required]
        [Display(Name = "templates")]
        public ICollection<EmailTemplateResponseDto> Templates { get; set; }
    }

    public class EmailTemplateResponseDto
    {
        [Required]
        public Dictionary<EmailParams, string> Params { get; set; }

        [Required]
        public EmailType Type { get; set; }

        [Required]
        [Display(Name = "localizations")]
        public ICollection<EmailConfigLocalizationResponseDto> Localizations { get; set; }
    }

    public class EmailConfigLocalizationResponseDto
    {
        [Required]
        public Language Language { get; set; }

        [Required]
        public int TemplateId { get; set; }
    }
}
