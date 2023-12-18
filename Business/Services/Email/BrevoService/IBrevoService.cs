using Common.Enums;
using Data.Entity;
using sib_api_v3_sdk.Model;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace Business.Services
{
    public interface IBrevoService
    {
        Task SendTransactional(SendSmtpEmail email, EmailType type, int templateId);

        Task Subscribe(UserEntity user);

        Task Unsubscribe(UserEntity user);

        Dictionary<UserParams, string> CreateContactParams(UserEntity user);
    }
}
