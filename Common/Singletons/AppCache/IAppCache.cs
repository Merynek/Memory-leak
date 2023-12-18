using Common.Dto;
using System.Collections.Generic;

namespace Common.Singletons
{
    public interface IAppCache
    {
        AppBusinessConfigResponseDto ConfigCache { get; }
        void CreateBanUserCache(List<UserDto> users);

        void SetAppConfig(AppBusinessConfigResponseDto config);

        void SetBan(int id, bool ban);

        bool isBanned(int id);
    }
}
