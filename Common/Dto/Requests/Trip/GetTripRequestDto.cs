using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class GetTripRequestDto
    {
        [Required]
        [FromQuery]
        [Display(Name = "tripId")]
        public int TripId { get; set; }
    }
}
