using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TripOfferResponseDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "user")]
        public UserDetailResponseDto User { get; set; }

        [Required]
        [Display(Name = "vehicle")]
        public VehicleResponseDto Vehicle { get; set; }

        [Required]
        [Display(Name = "price")]
        public PriceDto Price { get; set; }

        [Required]
        [Display(Name = "accepted")]
        public bool Accepted { get; set; }

        [Required]
        [Display(Name = "endOfferDate")]
        public DateTimeOffset EndOfferDate { get; set; }

        [Display(Name = "acceptOfferDate")]
        public DateTimeOffset? AcceptOfferDate { get; set; }
    }
}
