using Common.Enums;
using Task = System.Threading.Tasks.Task;

namespace Business.Services
{
    public interface IEmailService
    {
        Task SendTripCreate(int tripId, int addressUserId);

        Task SendNotificationNewOfferFromTransporter(int tripId, int addressUserId);

        Task SendOfferPayRestWarning(int tripId, int addressUserId);

        Task SendOfferPayWarning(int tripId, int addressUserId);

        Task SendTripOfferPayed25ForTransporter(int tripId, int addressUserId);

        Task SendTripOfferPayed25ForDemander(int tripId, int addressUserId, string proformalInvoiceBase64, string invoiceBase64);

        Task SendTripOfferPayed75ForTransporter(int tripId, int addressUserId);

        Task SendTripOfferPayed75ForDemander(int tripId, int addressUserId, string invoiceBase64);

        Task SendTripOfferPayed100ForTransporter(int tripId, int addressUserId);

        Task SendTripOfferPayed100ForDemander(int tripId, int addressUserId, string invoiceBase64);

        Task SendTripOfferClosedDemander(int tripId, int addressUserId, TripOfferChange changeType);

        Task SendTripOfferClosedTransporter(int tripId, int addressUserId, TripOfferChange changeType);
        Task SendTripOfferAcceptedDemander(int tripId, int addressUserId, string proformalInvoiceBase64);

        Task SendTripOfferAcceptedTransporter(int tripId, int addressUserId);

        Task SendTripDone(int tripId, int addressUserId);

        Task SendTripDoneNoOffers(int tripId, int addressUserId);

        Task SendTripCloseNoAcceptedOffer(int tripId, int addressUserId);

        Task SendPayCommissionForTripOfferForTransporter(int tripId, int addressUserId, string invoiceBase64);

        Task SendChangePassword(string email, string token);

        Task SendRegistration(int addressUserId, string token);
    }
}
