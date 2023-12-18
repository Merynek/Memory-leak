using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UpdateOfferRequestDto
    {
        [Required]
        [Display(Name = "offerId")]
        public int OfferId { get; set; }

        [Required]
        [Display(Name = "endOfferDate")]
        public DateTimeOffset EndOfferDate { get; set; }
    }
}
