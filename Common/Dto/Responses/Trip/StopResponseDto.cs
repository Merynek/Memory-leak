using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class StopResponseDto
    {
        [Required]
        public PlaceResponseDto Place { get; set; }

        [Required]
        public PlaceResolution Resolution { get; set; }
    }
}
