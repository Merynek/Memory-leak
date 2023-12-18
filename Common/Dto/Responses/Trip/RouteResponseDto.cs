using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class RouteResponseDto
    {
        [Required]
        [Display(Name = "start")]
        public DateTimeOffset Start { get; set; }

        [Required]
        [Display(Name = "from")]
        public StopResponseDto From { get; set; }

        [Required]
        [Display(Name = "to")]
        public StopResponseDto To { get; set; }

        [Required]
        [Display(Name = "end")]
        public DateTimeOffset End { get; set; }
    }
}
