using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class PlaceResponseDto
    {
        [Required]
        [Display(Name = "placeId")]
        public string PlaceId { get; set; }

        [Required]
        [Display(Name = "point")]
        public GeoPoint Point { get; set; }

        [Required]
        [Display(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "localityName")]
        public string LocalityName { get; set; }

        [Display(Name = "country")]
        public Country? Country { get; set; }
    }
}
