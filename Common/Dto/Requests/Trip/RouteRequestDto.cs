using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class RouteRequestDto
    {
        [Required]
        [Display(Name = "start")]
        public DateTimeOffset Start { get; set; }

        [Required]
        [Display(Name = "from")]
        public StopRequestDto From { get; set; }

        [Required]
        [Display(Name = "to")]
        public StopRequestDto To { get; set; }

        [Required]
        [Display(Name = "end")]
        public DateTimeOffset End { get; set; }
    }
}
