using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dto;
using Common.Enums;
using Data.Context;
using Data.Entity;
using GoogleApi.Entities.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class EmailConfigService : IEmailConfigService
    {
        private readonly SUBDbContext _context;
        private readonly IEmailParamsService _emailParamsService;

        public EmailConfigService(IEmailParamsService emailParamsService, SUBDbContext context)
        {
            _emailParamsService = emailParamsService;
            _context = context;
        }

        public async Task SetTemplate(UpdateEmailConfig req)
        {
            var template = await _context.EmailConfig
                .Where(c => c.Type == req.Type && c.Language == req.Language)
                .FirstOrDefaultAsync();

            if (template == null)
            {
                var newTemplate = new EmailConfigEntity();
                newTemplate.Language = req.Language;
                newTemplate.Type = req.Type;
                newTemplate.TemplateId = req.TemplateId;
                _context.EmailConfig.Add(newTemplate);
            }
            else
            {
                template.TemplateId = req.TemplateId;
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTemplate(EmailType type, Language language)
        {
            var template = await _context.EmailConfig
                .Where(c => c.Type == type && c.Language == language)
                .FirstOrDefaultAsync();

            if (template != null)
            {
                _context.EmailConfig.Remove(template);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<EmailTemplateResponseDto>> GetAllTemplates()
        {
            var responses = new List<EmailTemplateResponseDto>();
            var languages = new List<Language> { Language.Czech, Language.English };
            var templates = await _context.EmailConfig.ToListAsync();


            foreach (EmailType type in Enum.GetValues(typeof(EmailType)))
            {
                var response = new EmailTemplateResponseDto();
                var localizations = new List<EmailConfigLocalizationResponseDto>();

                foreach (Language language in languages)
                {
                    var template = templates.Where(c => c.Type == type && c.Language == language).FirstOrDefault();
                    response.Type = type;
                    response.Params = _createRandomParams(type);

                    var localization = new EmailConfigLocalizationResponseDto();
                    localization.Language = language;
                    localization.TemplateId = template == null ? 0 : template.TemplateId;
                    localizations.Add(localization);
                }
                response.Localizations = localizations;
                responses.Add(response);
            }

            return responses;
        }
        private Dictionary<EmailParams, string> _createRandomParams(EmailType emailType)
        {
            var trip = _createRandomTrip();
            switch (emailType)
            {
                case EmailType.FORGET_PASSWOD:
                    return _emailParamsService.CreateForgetPassword("123456");
                case EmailType.REGISTRATION:
                    return _emailParamsService.CreateRegistration("token1234");
                case EmailType.TRIP_CLOSE_NO_ACCEPTED_OFFER:
                    return _emailParamsService.CreateTripCloseNoAcceptedOffer(trip);
                case EmailType.TRIP_CREATE:
                    return _emailParamsService.CreateTripCreate(trip);
                case EmailType.TRIP_DONE:
                    return _emailParamsService.CreateTripDone(trip);
                case EmailType.TRIP_DONE_NO_OFFERS:
                    return _emailParamsService.CreateTripDoneNoOffers(trip);
                case EmailType.TRIP_NEW_OFFER_FROM_TRANSPORTER:
                    return _emailParamsService.CreateTripNewOfferFromTransporter(trip);
                case EmailType.TRIP_OFFER_ACCEPTED_100_DEMANDER:
                case EmailType.TRIP_OFFER_ACCEPTED_25_DEMANDER:
                    return _emailParamsService.CreateTripOfferAcceptedDemander(trip);
                case EmailType.TRIP_OFFER_ACCEPTED_TRANSPORTER:
                    return _emailParamsService.CreateTripOfferAcceptedTransporter(trip);
                case EmailType.TRIP_OFFER_CLOSED_DEMANDER:
                    return _emailParamsService.CreateTripOfferClosedDemander(trip);
                case EmailType.TRIP_OFFER_CLOSED_TRANSPORTER:
                    return _emailParamsService.CreateTripOfferClosedTransporter(trip);
                case EmailType.TRIP_OFFER_PAYED_100_FOR_DEMANDER:
                    return _emailParamsService.CreateTripOfferPayed100ForDemander(trip, 100);
                case EmailType.TRIP_OFFER_PAYED_100_FOR_TRANSPORTER:
                    return _emailParamsService.CreateTripOfferPayed100ForTransporter(trip, 100);
                case EmailType.TRIP_OFFER_PAYED_75_FOR_DEMANDER:
                    return _emailParamsService.CreateTripOfferPayed75ForDemander(trip, 75);
                case EmailType.TRIP_OFFER_PAYED_75_FOR_TRANSPORTER:
                    return _emailParamsService.CreateTripOfferPayed75ForTransporter(trip, 75);
                case EmailType.TRIP_OFFER_PAYED_25_FOR_DEMANDER:
                    return _emailParamsService.CreateTripOfferPayed25ForDemander(trip, 25);
                case EmailType.TRIP_OFFER_PAYED_25_FOR_TRANSPORTER:
                    return _emailParamsService.CreateTripOfferPayed25ForTransporter(trip, 25);
                case EmailType.TRIP_OFFER_PAY_WARNING_SET_25:
                case EmailType.TRIP_OFFER_PAY_WARNING_SET_100:
                case EmailType.TRIP_OFFER_PAY_WARNING_PAYED_25:
                    return _emailParamsService.CreateTripOfferPayWarning(trip);
                default: return new Dictionary<EmailParams, string>();
            }
        }

        private TripEntity _createRandomTrip()
        {
            var randomTrip = new TripEntity();
            randomTrip.Id = 1;
            return randomTrip;
        }
    }

}
