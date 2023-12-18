using Common.Enums;
using Common.Exceptions;
using Data.Context;
using Data.Entity;
using GoogleApi.Entities.Common.Enums;
using Microsoft.EntityFrameworkCore;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly SUBDbContext _context;
        private readonly IBrevoService _brevoService;
        private readonly IEmailParamsService _emailParamsService;

        public EmailService(IBrevoService brevoService, IEmailParamsService emailParamsService, SUBDbContext context)
        {
            _brevoService = brevoService;
            _emailParamsService = emailParamsService;
            _context = context;
        }

        public async Task SendTripCreate(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_CREATE;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripCreate(trip);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendNotificationNewOfferFromTransporter(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_NEW_OFFER_FROM_TRANSPORTER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripNewOfferFromTransporter(trip);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendOfferPayRestWarning(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            if (trip.OfferState == TripOfferState.PAYD_25)
            {
                var type = EmailType.TRIP_OFFER_PAY_WARNING_PAYED_25;
                var sendSmtpEmail = new SendSmtpEmail();
                sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
                sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayWarning(trip);
                await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
            }
        }

        public async Task SendOfferPayWarning(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            Func<EmailType, Task> _sendEmail = async (type) =>
            {
                var sendSmtpEmail = new SendSmtpEmail();
                sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
                sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayWarning(trip);
                await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
            };
            switch (trip.OfferState)
            {
                case TripOfferState.SET_TRANSPORTER_25:
                    await _sendEmail(EmailType.TRIP_OFFER_PAY_WARNING_SET_25);
                    break;
                case TripOfferState.SET_TRANSPORTER_100:
                    await _sendEmail(EmailType.TRIP_OFFER_PAY_WARNING_SET_100);
                    break;
                default:
                    return;
            }
        }

        public async Task SendTripOfferPayed25ForTransporter(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAYED_25_FOR_TRANSPORTER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayed25ForTransporter(trip, 25);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripOfferPayed25ForDemander(int tripId, int addressUserId, string proformalInvoiceBase64, string invoiceBase64)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAYED_25_FOR_DEMANDER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayed25ForDemander(trip, 25);
            var proformalInvoice = new SendSmtpEmailAttachment();
            proformalInvoice.Name = "Proformal_invoice.pdf";
            proformalInvoice.Content = Convert.FromBase64String(proformalInvoiceBase64);
            var invoice = new SendSmtpEmailAttachment();
            invoice.Name = "Invoice.pdf";
            invoice.Content = Convert.FromBase64String(invoiceBase64);
            sendSmtpEmail.Attachment = new List<SendSmtpEmailAttachment>() { proformalInvoice, invoice };
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripOfferPayed75ForTransporter(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAYED_75_FOR_TRANSPORTER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayed75ForTransporter(trip, 75);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripOfferPayed75ForDemander(int tripId, int addressUserId, string invoiceBase64)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAYED_75_FOR_DEMANDER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayed75ForDemander(trip, 75);
            var invoice = new SendSmtpEmailAttachment();
            invoice.Name = "Invoice.pdf";
            invoice.Content = Convert.FromBase64String(invoiceBase64);
            sendSmtpEmail.Attachment = new List<SendSmtpEmailAttachment>() { invoice };
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripOfferPayed100ForTransporter(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAYED_100_FOR_TRANSPORTER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayed100ForTransporter(trip, 100);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripOfferPayed100ForDemander(int tripId, int addressUserId, string invoiceBase64)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAYED_100_FOR_DEMANDER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferPayed100ForDemander(trip, 100);
            var invoice = new SendSmtpEmailAttachment();
            invoice.Name = "Invoice.pdf";
            invoice.Content = Convert.FromBase64String(invoiceBase64);
            sendSmtpEmail.Attachment = new List<SendSmtpEmailAttachment>() { invoice };
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripOfferClosedDemander(int tripId, int addressUserId, TripOfferChange changeType)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            Func<EmailType, Task> _sendEmail = async (type) =>
            {
                var sendSmtpEmail = new SendSmtpEmail();
                sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
                sendSmtpEmail.Params = _emailParamsService.CreateTripOfferClosedDemander(trip);
                await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
            };

            switch (changeType)
            {
                case TripOfferChange.UNPAID_25:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_UNPAID_25_DEMANDER);
                    break;
                case TripOfferChange.UNPAID_75:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_UNPAID_75_DEMANDER);
                    break;
                case TripOfferChange.UNPAID_100:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_UNPAID_100_DEMANDER);
                    break;
                case TripOfferChange.CLOSE:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_DEMANDER);
                    break;
                default:
                    return;
            }
        }

        public async Task SendTripOfferClosedTransporter(int tripId, int addressUserId, TripOfferChange changeType)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            Func<EmailType, Task> _sendEmail = async (type) =>
            {
                var sendSmtpEmail = new SendSmtpEmail();
                sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
                sendSmtpEmail.Params = _emailParamsService.CreateTripOfferClosedTransporter(trip);
                await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
            };

            switch (changeType)
            {
                case TripOfferChange.UNPAID_25:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_UNPAID_25_TRANSPORTER);
                    break;
                case TripOfferChange.UNPAID_75:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_UNPAID_75_TRANSPORTER);
                    break;
                case TripOfferChange.UNPAID_100:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_UNPAID_100_TRANSPORTER);
                    break;
                case TripOfferChange.CLOSE:
                    await _sendEmail(EmailType.TRIP_OFFER_CLOSED_TRANSPORTER);
                    break;
                default:
                    return;
            }
        }

        public async Task SendTripOfferAcceptedDemander(int tripId, int addressUserId, string proformalInvoiceBase64)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var demanderType = trip.OfferState == TripOfferState.SET_TRANSPORTER_25 ? EmailType.TRIP_OFFER_ACCEPTED_25_DEMANDER : EmailType.TRIP_OFFER_ACCEPTED_100_DEMANDER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferAcceptedDemander(trip);
            var proformalInvoice = new SendSmtpEmailAttachment();
            proformalInvoice.Name = "Proformal_invoice.pdf";
            proformalInvoice.Content = Convert.FromBase64String(proformalInvoiceBase64);
            sendSmtpEmail.Attachment = new List<SendSmtpEmailAttachment>() { proformalInvoice };
            await _brevoService.SendTransactional(sendSmtpEmail, demanderType, await _getTemplateId(demanderType));
        }

        public async Task SendTripOfferAcceptedTransporter(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_ACCEPTED_TRANSPORTER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripOfferAcceptedTransporter(trip);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripDone(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_DONE;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripDone(trip);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripDoneNoOffers(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_DONE_NO_OFFERS;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripDoneNoOffers(trip);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendTripCloseNoAcceptedOffer(int tripId, int addressUserId)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_CLOSE_NO_ACCEPTED_OFFER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripCloseNoAcceptedOffer(trip);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendPayCommissionForTripOfferForTransporter(int tripId, int addressUserId, string invoiceBase64)
        {
            var addressee = await _getUser(addressUserId);
            var trip = await _getTrip(tripId);

            var type = EmailType.TRIP_OFFER_PAY_COMMISSION_TRANSPORTER;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateTripCloseNoAcceptedOffer(trip);
            var invoice = new SendSmtpEmailAttachment();
            invoice.Name = "Invoice.pdf";
            invoice.Content = Convert.FromBase64String(invoiceBase64);
            sendSmtpEmail.Attachment = new List<SendSmtpEmailAttachment>() { invoice };
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendChangePassword(string email, string token)
        {
            var type = EmailType.FORGET_PASSWOD;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email) };
            sendSmtpEmail.Params = _emailParamsService.CreateForgetPassword(token);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        public async Task SendRegistration(int addressUserId, string token)
        {
            var addressee = await _getUser(addressUserId);

            var type = EmailType.REGISTRATION;
            var sendSmtpEmail = new SendSmtpEmail();
            sendSmtpEmail.To = new List<SendSmtpEmailTo> { new SendSmtpEmailTo(addressee.Email) };
            sendSmtpEmail.Params = _emailParamsService.CreateRegistration(token);
            await _brevoService.SendTransactional(sendSmtpEmail, type, await _getTemplateId(type));
        }

        private async Task<int> _getTemplateId(EmailType type, Language language = Language.Czech)
        {
            var config = await _context.EmailConfig
                   .Where(c => c.Type == type && c.Language == language)
                   .FirstOrDefaultAsync();

            if (config == null)
            {
                throw new InternalErrorSUBException(ErrorCode.UNKNOWN, "Email not found: Type: " + type.ToString() + " language: " + language.ToString());
            }
            return config.TemplateId;
        }

        private async Task<UserEntity> _getUser(int userId)
        {
            var user = await _context.User
                .Where(user => user.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new InternalErrorSUBException(ErrorCode.UNKNOWN, "User not found on send Email: userId: " + userId.ToString());
            }
            return user;
        }

        private async Task<TripEntity> _getTrip(int tripId)
        {
            var trip = await _context.Trip
                .Where(trip => trip.Id == tripId)
                .FirstOrDefaultAsync();

            if (trip == null)
            {
                throw new InternalErrorSUBException(ErrorCode.UNKNOWN, "Trip not found on send Email: tripId: " + tripId.ToString());
            }
            return trip;
        }
    }

}
