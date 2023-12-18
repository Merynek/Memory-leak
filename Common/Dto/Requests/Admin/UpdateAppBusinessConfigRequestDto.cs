using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class UpdateAppBusinessConfigRequestDto
    {
        [Required]
        [Display(Name = "minEndOrderFromNowInHours")]
        public double MinEndOrderFromNowInHours { get; set; }

        [Required]
        [Display(Name = "minDateToAcceptOfferInHours")]
        public double MinDateToAcceptOfferInHours { get; set; }

        [Required]
        [Display(Name = "minDiffBetweenStartTripAndEndOrderForAllPaymentsInHours")]
        public double MinDiffBetweenStartTripAndEndOrderForAllPaymentsInHours { get; set; }

        [Required]
        [Display(Name = "minDiffBetweenStartTripAndEndOrderInHours")]
        public double MinDiffBetweenStartTripAndEndOrderInHours { get; set; }

        [Required]
        [Display(Name = "payInvoiceWarningAfterAcceptOfferInHours")]
        public double PayInvoiceWarningAfterAcceptOfferInHours { get; set; }

        [Required]
        [Display(Name = "payRestOfPriceWarningBeforeStartTripInHours")]
        public double PayRestOfPriceWarningBeforeStartTripInHours { get; set; }

        [Required]
        [Display(Name = "payCommissionAfterTripInHours")]
        public double PayCommissionAfterTripInHours { get; set; }        
    }
}
