using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class CreateTripRequestDto
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "routes")]
        public IEnumerable<RouteRequestDto> Routes { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "numberOfPersons")]
        public int NumberOfPersons { get; set; }

        [Required]
        [Display(Name = "amenities")]
        public List<Amenities> Amenities { get; set; }

        [Required]
        [Display(Name = "handicappedUserCount")]
        public int HandicappedUserCount { get; set; }

        [Required]
        [Display(Name = "dietForTransporter")]
        public Boolean DietForTransporter { get; set; }

        [Required]
        [Display(Name = "endOrder")]
        public DateTimeOffset EndOrder { get; set; }

        [Required]
        [Display(Name = "directions")]
        public IEnumerable<DirectionRequestDto> Directions { get; set; }
    }
}
