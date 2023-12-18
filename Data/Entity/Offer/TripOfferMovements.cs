using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class TripOfferMovements
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public TripOfferState From { get; set; }
        public TripOfferState To { get; set; }
        public DateTimeOffset Datetime { get; set; }

        public TripOfferMovements()
        { }

        public TripOfferMovementsResponseDto convertToResponse()
        {
            var response = new TripOfferMovementsResponseDto();
            response.Id = this.Id;
            response.TripId = this.TripId;
            response.From = this.From;
            response.To = this.To;
            response.Datetime = this.Datetime;
            return response;
        }
    }
}
