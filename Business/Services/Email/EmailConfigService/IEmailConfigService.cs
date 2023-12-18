using Common.Dto;
using Common.Enums;
using GoogleApi.Entities.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IEmailConfigService
    {
        Task SetTemplate(UpdateEmailConfig req);

        Task RemoveTemplate(EmailType type, Language language);

        Task<List<EmailTemplateResponseDto>> GetAllTemplates();
    }
}
