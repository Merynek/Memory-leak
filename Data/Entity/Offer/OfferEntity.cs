using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entity
{
    public class OfferEntity
    {
        public int Id { get; set; }        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public Currency Currency { get; set; }
        public bool Accepted { get; set; }
        public DateTimeOffset EndOfferDate { get; set; }
        public DateTimeOffset? AcceptOfferDate { get; set; }

        // relations
        public int? UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public int? TripId { get; set; }
        public virtual TripEntity Trip { get; set; }
        public int? VehicleId { get; set; }

        public OfferEntity()
        { }

        public TripOfferResponseDto convertToResponse()
        {
            var response = new TripOfferResponseDto();
            response.Id = this.Id;
            response.Price = new PriceDto(this.Price, this.Currency);
            response.Vehicle = null;
            response.User = this.User.convertToResponse();
            response.EndOfferDate = this.EndOfferDate;
            response.Accepted = this.Accepted;
            response.AcceptOfferDate = this.AcceptOfferDate;
            return response;
        }
    }
}
