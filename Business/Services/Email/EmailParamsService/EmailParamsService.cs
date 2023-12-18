using Common.AppSettings;
using Common.Enums;
using Data.Entity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Business.Services
{
    public class EmailParamsService : IEmailParamsService
    {
        private readonly ClientSettings _clientSettings;

        public EmailParamsService(IOptions<ClientSettings> clientSettings)
        {
            _clientSettings = clientSettings.Value;
        }


        private string _createTripLink(int tripId)
        {
            return _clientSettings.Url + "/trip/" + tripId;
        }

        public Dictionary<EmailParams, string> CreateTripCreate(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripNewOfferFromTransporter(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayWarning(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayed25ForTransporter(TripEntity trip, int percent)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            dic.Add(EmailParams.PERCENT, percent.ToString());
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayed25ForDemander(TripEntity trip, int percent)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            dic.Add(EmailParams.PERCENT, percent.ToString());
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayed75ForTransporter(TripEntity trip, int percent)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            dic.Add(EmailParams.PERCENT, percent.ToString());
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayed75ForDemander(TripEntity trip, int percent)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            dic.Add(EmailParams.PERCENT, percent.ToString());
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayed100ForTransporter(TripEntity trip, int percent)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            dic.Add(EmailParams.PERCENT, percent.ToString());
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferPayed100ForDemander(TripEntity trip, int percent)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            dic.Add(EmailParams.PERCENT, percent.ToString());
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferClosedDemander(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferClosedTransporter(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferAcceptedDemander(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripOfferAcceptedTransporter(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripDone(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }


        public Dictionary<EmailParams, string> CreateTripDoneNoOffers(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateTripCloseNoAcceptedOffer(TripEntity trip)
        {
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.TRIP_DETAIL_LINK, _createTripLink(trip.Id));
            return dic;
        }

        public Dictionary<EmailParams, string> CreateForgetPassword(string token)
        {
            var link = _clientSettings.Url + "/reset_password/" + token;
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.RESET_PASSWORD_LINK, link);
            return dic;
        }


        public Dictionary<EmailParams, string> CreateRegistration(string token)
        {
            var link = _clientSettings.Url + "/activeUser/" + token;
            var dic = new Dictionary<EmailParams, string>();
            dic.Add(EmailParams.ACTIVE_USER_LINK, link);
            return dic;
        }

    }
}
