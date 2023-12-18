using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TripRecommendationRequestDto
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "routes")]
        public IEnumerable<TripRecommendationRouteRequestDto> Routes { get; set; }
    }

    public class TripRecommendationRouteRequestDto
    {
        [Required]
        [Display(Name = "directionTimeSeconds")]
        public double directionTimeSeconds { get; set; }

        [Required]
        [Display(Name = "previousPauseTimeSeconds")]
        public double previousPauseTimeSeconds { get; set; }
    }
}
