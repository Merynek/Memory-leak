using Common.Enums;
using Data.Entity;
using System;

namespace Business.Tools
{
    public static class TripOfferStateMovementTool
    {
        public static TripOfferMovements CreateTripOfferStateMovement(TripEntity trip, TripOfferState newState)
        {
            var offerMovement = new TripOfferMovements();
            offerMovement.TripId = trip.Id;
            offerMovement.Datetime = DateTimeOffset.UtcNow;
            offerMovement.From = trip.OfferState;
            offerMovement.To = newState;
            return offerMovement;
        }
    }
}
