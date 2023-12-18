using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class StopRequestDto
    {
        [Required]
        [Display(Name = "placeId")]
        public PlaceRequestDto Place { get; set; }

        [Required]
        [Display(Name = "resolution")]
        public PlaceResolution Resolution { get; set; }
    }
}
