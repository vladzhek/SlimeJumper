using System;
using UnityEngine;
using YG;
using Zenject;

namespace Script.Gameplay.Services
{
    public class AdsService : IInitializable
    {
        private CurrencyModel _currencyModel;
        
        public AdsService(CurrencyModel currencyModel)
        {
            _currencyModel = currencyModel;
        }

        public void Initialize()
        {
            YandexGame.RewardVideoEvent += UserGotReward;
        }
        
        public void ShowFullScreenBanner()
        {
            YandexGame.FullscreenShow();
        }

        public void ShowReward()
        {
            YandexGame.RewVideoShow(0);
        }

        private void UserGotReward(int id_reward)
        {
            Debug.Log("REWARDED +5");
            _currencyModel.AddCurrency(CurrencyType.Hard, 5);
        }
    }
}