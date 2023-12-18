using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.AppSettings;
using Common.Enums;
using Common.Exceptions;
using Data.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;

namespace Business.Services
{
    public class BrevoService : IBrevoService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<BrevoService> _logger;

        public BrevoService(IOptions<EmailSettings> emailSettings, ILogger<BrevoService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
            Configuration.Default.AddApiKey("api-key", _emailSettings.BrevoApiKey);
        }

        public async Task SendTransactional(SendSmtpEmail email, EmailType type, int templateId)
        {
            var apiInstance = new TransactionalEmailsApi();
            email.TemplateId = templateId;
            try
            {
                CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(email);
                _logger.LogInformation("Email sent: Type: " + type.ToString() + " Id: " + email.TemplateId, result.ToString());
            }
            catch (Exception e)
            {
                throw new InternalErrorSUBException(ErrorCode.UNKNOWN, "Email Fail: Type: " + type.ToString() + " ID: " + email.TemplateId + " msg: " + e.Message);
            }
        }

        public async Task Subscribe(UserEntity user)
        {
            var info = await _getContactInfo(user.Email);
            if (info == null)
            {
                await _createContact(user);
            }
            else
            {
                await _updateContact(user);
            }
        }

        public async Task Unsubscribe(UserEntity user)
        {
            var info = await _getContactInfo(user.Email);

            if (info != null)
            {
                var apiInstance = new ContactsApi();
                await apiInstance.DeleteContactAsync(user.Email);
            }
        }


        private async Task _createContact(UserEntity user)
        {
            var apiInstance = new ContactsApi();
            var contact = new CreateContact();
            contact.Email = user.Email;
            contact.Attributes = CreateContactParams(user);

            try
            {
                await apiInstance.CreateContactAsync(contact);
            }
            catch (Exception e)
            {
                throw new InternalErrorSUBException(ErrorCode.UNKNOWN, "Create Contact Sendinblue fail: Type: " + user.Email + " msg: " + e.Message);
            }
        }

        private async Task _updateContact(UserEntity user)
        {
            var apiInstance = new ContactsApi();
            var contact = new UpdateContact();
            contact.Attributes = CreateContactParams(user);

            try
            {
                await apiInstance.UpdateContactAsync(user.Email, contact);
            }
            catch (Exception e)
            {
                throw new InternalErrorSUBException(ErrorCode.UNKNOWN, "Update Contact Sendinblue fail: Type: " + user.Email + " msg: " + e.Message);
            }
        }

        private async Task<GetExtendedContactDetails> _getContactInfo(string email)
        {
            var apiInstance = new ContactsApi();
            try
            {
                return await apiInstance.GetContactInfoAsync(email);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Dictionary<UserParams, string> CreateContactParams(UserEntity user)
        {
            var dic = new Dictionary<UserParams, string>();
            dic.Add(UserParams.Name, user.Name);
            dic.Add(UserParams.Surname, user.Surname);
            dic.Add(UserParams.PhoneNumber, user.PhoneNumber);
            dic.Add(UserParams.Role, user.Role.ToString());
            return dic;
        }
    }

}
