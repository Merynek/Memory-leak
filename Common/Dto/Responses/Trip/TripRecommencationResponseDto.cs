using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TripRecommendationResponseDto
    {
        [Required]
        public TripRecommendationType type { get; set; }

        [Required]
        public ICollection<TripRecommendationRouteResponseDto> routes { get; set; }

        [Required]
        public double reduce_routes_hours { get; set; } = 0;

        [Required]
        public double reduce_time_hours { get; set; } = 0;
    }

    public class TripRecommendationRouteResponseDto
    {
        [Required]
        public double DJ_InHours { get; set; }

        [Required]
        public double M_InHours { get; set; }

        [Required]
        public double Real_Time_InHours { get; set; }
    }
}
