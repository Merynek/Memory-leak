using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class DirectionResponseDto
    {
        [Required]
        [Display(Name = "time")]
        public int Time { get; set; }

        [Required]
        [Display(Name = "distance")]
        public int Distance { get; set; }

        [Required]
        [Display(Name = "transportType")]
        public TransportType TransportType { get; set; }

        [Required]
        [Display(Name = "polyline")]
        public string Polyline { get; set; }

        [Required]
        [Display(Name = "points")]
        public ICollection<GeoPoint> Points { get; set; }
    }
}
