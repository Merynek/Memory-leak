using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class AcceptedTripResponseDto
    {
        [Required]
        public int TripId { get; set; }

        [Required]
        public TripOfferState OfferState { get; set; }

        [Required]
        public ICollection<TripInvoiceResponseDto> invoices { get; set; } = new List<TripInvoiceResponseDto>();

        public TripOfferResponseDto? Offer { get; set; }
    }
}
