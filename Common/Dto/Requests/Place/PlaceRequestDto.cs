using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class PlaceRequestDto
    {
        [FromQuery]
        [Required]
        [Display(Name = "placeId")]
        public string PlaceId { get; set; }
    }
}
