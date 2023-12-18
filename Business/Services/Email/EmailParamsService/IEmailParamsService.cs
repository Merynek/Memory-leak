using Common.Enums;
using Data.Entity;
using System.Collections.Generic;

namespace Business.Services
{
    public interface IEmailParamsService
    {
        Dictionary<EmailParams, string> CreateTripCreate(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripNewOfferFromTransporter(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripOfferPayWarning(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripOfferPayed25ForTransporter(TripEntity trip, int percent);

        Dictionary<EmailParams, string> CreateTripOfferPayed25ForDemander(TripEntity trip, int percent);

        Dictionary<EmailParams, string> CreateTripOfferPayed75ForTransporter(TripEntity trip, int percent);

        Dictionary<EmailParams, string> CreateTripOfferPayed75ForDemander(TripEntity trip, int percent);

        Dictionary<EmailParams, string> CreateTripOfferPayed100ForTransporter(TripEntity trip, int percent);

        Dictionary<EmailParams, string> CreateTripOfferPayed100ForDemander(TripEntity trip, int percent);

        Dictionary<EmailParams, string> CreateTripOfferClosedDemander(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripOfferClosedTransporter(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripOfferAcceptedDemander(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripOfferAcceptedTransporter(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripDone(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripDoneNoOffers(TripEntity trip);

        Dictionary<EmailParams, string> CreateTripCloseNoAcceptedOffer(TripEntity trip);

        Dictionary<EmailParams, string> CreateForgetPassword(string token);

        Dictionary<EmailParams, string> CreateRegistration(string token);
    }
}
