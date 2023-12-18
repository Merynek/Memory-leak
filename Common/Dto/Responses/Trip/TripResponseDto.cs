using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class TripResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public ICollection<RouteResponseDto> Routes { get; set; }

        [Required]
        public ICollection<DirectionResponseDto> Directions { get; set; }

        [Required]
        public ICollection<Amenities> Amenities { get; set; }

        [Required]
        public int NumberOfPersons { get; set; }

        [Required]
        public int HandicappedUserCount { get; set; }

        [Required]
        public int TotalDistanceInMeters { get; set; }

        [Required]
        public Boolean DietForTransporter { get; set; }

        [Required]
        public DateTimeOffset EndOrder { get; set; }

        [Required]
        public bool IsDone { get; set; }

        [Required]
        public TripOfferState OfferState { get; set; }

        [Required]
        public bool IsMine { get; set; }

        [Required]
        public ICollection<TripOfferResponseDto> Offers { get; set; } = new List<TripOfferResponseDto>() { };
    }
}
