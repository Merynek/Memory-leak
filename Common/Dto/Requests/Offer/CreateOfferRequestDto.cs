using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class CreateOfferRequestDto
    {
        [Required]
        [Display(Name = "endOfferDate")]
        public DateTimeOffset EndOfferDate { get; set; }

        [Required]
        [Display(Name = "tripId")]
        public int TripId { get; set; }

        [Required]
        [Display(Name = "vehicleId")]
        public int VehicleId { get; set; }

        [Required]
        [Display(Name = "price")]
        public PriceDto Price { get; set; }
    }
}
