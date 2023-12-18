using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class AcceptOfferRequestDto
    {
        [Required]
        [Display(Name = "offerId")]
        public int OfferId { get; set; }

        [Required]
        [Display(Name = "acceptMethod")]
        public TripOfferAcceptMethod AcceptMethod { get; set; }
    }
}
