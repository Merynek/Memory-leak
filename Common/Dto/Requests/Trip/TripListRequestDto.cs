using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class TripListRequestDto
    {
        [Required]
        [FromQuery]
        [Display(Name = "limit")]
        public int Limit { get; set; }

        [Required]
        [FromQuery]
        [Display(Name = "offset")]
        public int Offset { get; set; }

        [FromQuery]
        [Display(Name = "start")]
        public DateTimeOffset? Start { get; set; }

        [FromQuery]
        [Display(Name = "maxNumberOfPersons")]
        public int? MaxNumberOfPersons { get; set; }

        [FromQuery]
        [Display(Name = "dietForTransporter")]
        public bool? DietForTransporter { get; set; }

        [FromQuery]
        [Display(Name = "onlyMine")]
        public bool? OnlyMine { get; set; }

        [FromQuery]
        [Display(Name = "meOffered")]
        public bool? MeOffered { get; set; }

        [FromQuery]
        [Display(Name = "distanceFromInMeters")]
        public int? DistanceFromInMeters { get; set; }

        [FromQuery]
        [Display(Name = "distanceToInMeters")]
        public int? DistanceToInMeters { get; set; }
    }
}
