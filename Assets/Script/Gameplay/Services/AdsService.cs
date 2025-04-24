using GoogleMobileAds.Api;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using UnityEngine;
using Zenject;

namespace Script.Gameplay.Services
{
    public class AdsService : IInitializable
    {
        private CurrencyModel _currencyModel;
        private UIManager _uiManager;

#if UNITY_ANDROID
        private const string BannerUnitId = "ca-app-pub-4084447808428710/9982874174";//"ca-app-pub-3940256099942544/6300978111";
        private const string RewardedUnitId = "ca-app-pub-4084447808428710/3110785003";//"ca-app-pub-3940256099942544/5224354917";
#else
        private const string BannerUnitId = "unused";
#endif

        private BannerView _bannerView;
        private RewardedAd _rewardedAd;

        public AdsService(CurrencyModel currencyModel, UIManager uiManager)
        {
            _currencyModel = currencyModel;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            MobileAds.Initialize(initStatus => Debug.Log("[AdsService] Google Ads инициализирован"));
        }

        public void ShowBanner()
        {
            Debug.Log("[AdsService] Показ баннера...");

            DisposeBanner();

            _bannerView = new BannerView(BannerUnitId, AdSize.Banner, AdPosition.Bottom);

            _bannerView.OnBannerAdLoaded += () =>
            {
                Debug.Log("[AdsService] Баннер загружен");
                _bannerView.Show();
            };

            _bannerView.OnBannerAdLoadFailed += error =>
            {
                Debug.LogError($"[AdsService] Ошибка загрузки баннера: {error.GetMessage()}");
            };

            _bannerView.LoadAd(new AdRequest());
        }

        public void ShowReward()
        {
            Debug.Log("[AdsService] Загружаем rewarded ad...");

            RewardedAd.Load(RewardedUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null)
                {
                    Debug.LogError($"[AdsService] Не удалось загрузить rewarded ad: {error.GetMessage()}");
                    return;
                }

                _rewardedAd = ad;

                RegisterRewardedEvents(_rewardedAd);

                if (_rewardedAd.CanShowAd())
                {
                    Debug.Log("[AdsService] Показываем rewarded ad...");
                    _rewardedAd.Show(reward =>
                    {
                        Debug.Log($"[AdsService] Награда получена: {reward.Type} ({reward.Amount})");
                        UserGotReward(); // Выдача награды
                    });
                }
                else
                {
                    Debug.LogWarning("[AdsService] Rewarded ad не готов.");
                }
            });
        }

        private void RegisterRewardedEvents(RewardedAd ad)
        {
            ad.OnAdFullScreenContentOpened += () =>
                Debug.Log("[AdsService] Rewarded Ad открыт");

            ad.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("[AdsService] Rewarded Ad закрыт");
                ad.Destroy();
                _rewardedAd = null;
            };

            ad.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError($"[AdsService] Ошибка показа rewarded ad: {error.GetMessage()}");
                _rewardedAd = null;
            };

            ad.OnAdPaid += adValue =>
                Debug.Log($"[AdsService] Ad оплачен: {adValue.Value}");
        }

        private void UserGotReward()
        {
            Debug.Log("[AdsService] Выдача награды: +5 Hard");
            _currencyModel.AddCurrency(CurrencyType.Hard, 5);
        }

        private void DisposeBanner()
        {
            if (_bannerView != null)
            {
                _bannerView.Destroy();
                _bannerView = null;
                Debug.Log("[AdsService] Баннер уничтожен");
            }
        }

        public void Dispose()
        {
            DisposeBanner();
            _rewardedAd?.Destroy();
            _rewardedAd = null;
        }
    }
}
