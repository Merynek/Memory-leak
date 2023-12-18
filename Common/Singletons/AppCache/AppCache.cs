using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;

namespace Common.Singletons
{
    public class AppCache : IAppCache
    {
        private ConcurrentDictionary<int, bool> _banCache = new ConcurrentDictionary<int, bool>();

        public AppBusinessConfigResponseDto ConfigCache { get; } = new AppBusinessConfigResponseDto();

        public void CreateBanUserCache(List<UserDto> users)
        {
            foreach (var user in users)
            {
                _banCache.TryAdd(user.Id, user.Banned);
            }
        }

        public void SetAppConfig(AppBusinessConfigResponseDto config)
        {
            ConfigCache.MinEndOrderFromNowInHours = config.MinEndOrderFromNowInHours;
            ConfigCache.MinDiffBetweenStartTripAndEndOrderInHours = config.MinDiffBetweenStartTripAndEndOrderInHours;
            ConfigCache.MinDateToAcceptOfferInHours = config.MinDateToAcceptOfferInHours;
            ConfigCache.MinDiffBetweenStartTripAndEndOrderForAllPaymentsInHours = config.MinDiffBetweenStartTripAndEndOrderForAllPaymentsInHours;
            ConfigCache.PayInvoiceWarningAfterAcceptOfferInHours = config.PayInvoiceWarningAfterAcceptOfferInHours;
            ConfigCache.PayRestOfPriceWarningBeforeStartTripInHours = config.PayRestOfPriceWarningBeforeStartTripInHours;
            ConfigCache.PayCommissionAfterTripInHours = config.PayCommissionAfterTripInHours;
        }

        public void SetBan(int id, bool ban)
        {
            if (_banCache.ContainsKey(id))
            {
                _banCache[id] = ban;
            }
            else
            {
                _banCache.TryAdd(id, ban);
            }
            _refreshBanCache();
        }

        public bool isBanned(int id)
        {
            return _banCache.ContainsKey(id);
        }

        private void _refreshBanCache()
        {
            foreach (var item in _banCache.Where(banned => !banned.Value))
            {
                _banCache.Remove(item.Key, out var x);
            }
        }
    }

}
