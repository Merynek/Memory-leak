using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Entity
{
    public class TripEntity
    {
        public int Id { get; set; }        
        public ICollection<Amenities> Amenities { get; set; }        
        public int NumberOfPersons { get; set; }
        public Boolean DietForTransporter { get; set; }
        public int HandicappedUserCount { get; set; }
        public int TotalDistanceInMeters { get; set; } = 0;
        public DateTimeOffset EndOrder { get; set; }
        public bool IsDone { get; set; } = false;
        public TripOfferState OfferState { get; set; } = TripOfferState.NO_TRANSPORTER;
        public DateTimeOffset StartTrip { get; set; }
        public DateTimeOffset EndTrip { get; set; }
        public DateTimeOffset Created { get; set; }        

        // relations
        public int? OwnerId { get; set; }
        public virtual UserEntity Owner { get; set; }
        public virtual ICollection<RouteEntity> Routes { get; set; }
        public virtual ICollection<OfferEntity> Offers { get; set; }
        public virtual ICollection<DirectionEntity> Directions { get; set; }

        public void InitEntity(CreateTripRequestDto req)
        {
            this.Routes = req.Routes.Select(s => {
                var r = new RouteEntity();
                r.InitEntity(s);
                return r;
            }).ToList();


            this.NumberOfPersons = req.NumberOfPersons;
            this.Amenities = req.Amenities;
            this.DietForTransporter = req.DietForTransporter;
            this.HandicappedUserCount = req.HandicappedUserCount;
            this.EndOrder = req.EndOrder;
            this.Directions = req.Directions.Select(d => {
                var entity = new DirectionEntity();
                entity.InitEntity(d);
                return entity;
            }).ToList();
            this.IsDone = false;
            this.OfferState = TripOfferState.NO_TRANSPORTER;
            this.TotalDistanceInMeters = _computeTotalDistanceInMeters();
            this.StartTrip = req.Routes.First().Start;
            this.EndTrip = req.Routes.Last().End;
            this.Created = DateTimeOffset.UtcNow;
        }

        public TripEntity()
        {}

        public TripResponseDto convertToResponse()
        {
            var trip = new TripResponseDto();
            trip.Id = this.Id;
            trip.Amenities = this.Amenities;
            trip.DietForTransporter = this.DietForTransporter;
            trip.NumberOfPersons = this.NumberOfPersons;
            trip.TotalDistanceInMeters = this.TotalDistanceInMeters;
            trip.Routes = this.Routes.Select(r => r.ConvertToResponse()).ToList();
            trip.Directions = this.Directions.Select(d => d.convertToResponse()).ToList();
            trip.EndOrder = this.EndOrder;
            trip.IsDone = this.IsDone;
            trip.OfferState = this.OfferState;
            trip.HandicappedUserCount = this.HandicappedUserCount;          
            return trip;
        }

        public TripItemResponseDto convertToItemResponse()
        {
            var trip = new TripItemResponseDto();
            trip.Id = this.Id;
            trip.Amenities = this.Amenities;
            trip.DietForTransporter = this.DietForTransporter;
            trip.NumberOfPersons = this.NumberOfPersons;
            trip.TotalDistanceInMeters = this.TotalDistanceInMeters;
            trip.Routes = this.Routes.Select(r => r.ConvertToResponse()).ToList();
            trip.EndOrder = this.EndOrder;
            trip.IsDone = this.IsDone;
            trip.OfferState = this.OfferState;
            trip.HandicappedUserCount = this.HandicappedUserCount;
            return trip;
        }

        private int _computeTotalDistanceInMeters()
        {
            return this.Directions.Select(d => d.Distance).Sum();
        }
    }
}
