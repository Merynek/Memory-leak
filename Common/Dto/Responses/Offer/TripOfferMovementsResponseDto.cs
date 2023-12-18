using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TripOfferMovementsResponseDto
    {
        [Required]
        [Display(Name = "id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "tripId")]
        public int TripId { get; set; }

        [Required]
        [Display(Name = "from")]
        public TripOfferState From { get; set; }

        [Required]
        [Display(Name = "to")]
        public TripOfferState To { get; set; }

        [Required]
        [Display(Name = "datetime")]
        public DateTimeOffset Datetime { get; set; }
    }
}
