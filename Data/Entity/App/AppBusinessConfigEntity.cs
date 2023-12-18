using Common.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entity
{
    public class AppBusinessConfigEntity
    {
        [DefaultValue("1")]
        [Key]
        public int id { get; set; }

        // Create trip
        public double MinEndOrderFromNowInHours { get; set; } = 24;
        public double MinDiffBetweenStartTripAndEndOrderInHours { get; set; } = 480;

        // Accept offer
        public double MinDateToAcceptOfferInHours { get; set; } = 24;
        public double MinDiffBetweenStartTripAndEndOrderForAllPaymentsInHours { get; set; } = 600;

        // Notifications
        public double PayInvoiceWarningAfterAcceptOfferInHours { get; set; } = 72;

        public double PayRestOfPriceWarningBeforeStartTripInHours { get; set; } = 480;

        // After trip
        public double PayCommissionAfterTripInHours { get; set; } = 168;

        public AppBusinessConfigEntity()
        { }

        public AppBusinessConfigResponseDto ConvertToResponse()
        {
            var cfg = new AppBusinessConfigResponseDto();
            cfg.MinDateToAcceptOfferInHours = this.MinDateToAcceptOfferInHours;
            cfg.MinEndOrderFromNowInHours = this.MinEndOrderFromNowInHours;
            cfg.MinDiffBetweenStartTripAndEndOrderForAllPaymentsInHours = this.MinDiffBetweenStartTripAndEndOrderForAllPaymentsInHours;
            cfg.MinDiffBetweenStartTripAndEndOrderInHours = this.MinDiffBetweenStartTripAndEndOrderInHours;

            // Notifications
            cfg.PayInvoiceWarningAfterAcceptOfferInHours = this.PayInvoiceWarningAfterAcceptOfferInHours;
            cfg.PayRestOfPriceWarningBeforeStartTripInHours = this.PayRestOfPriceWarningBeforeStartTripInHours;
            cfg.PayCommissionAfterTripInHours = this.PayCommissionAfterTripInHours;
            return cfg;
        }
    }
}
