using System;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using UnityEngine;
using YG;
using Zenject;

namespace Script.Gameplay.Services
{
    public class AdsService : IInitializable
    {
        private CurrencyModel _currencyModel;
        private UIManager _uiManager;
        
        public AdsService(CurrencyModel currencyModel, UIManager uiManager)
        {
            _currencyModel = currencyModel;
            _uiManager = uiManager;
        }

        public void Initialize()
        {
            YandexGame.RewardVideoEvent += UserGotReward;
            YandexGame.OpenVideoEvent += OpenEmpty;
            YandexGame.CloseVideoEvent += CloseEmpty;
            YandexGame.ErrorVideoEvent += CloseEmpty;
            YandexGame.ErrorFullAdEvent += CloseEmpty;
        }
        
        private void OpenEmpty()
        {
            _uiManager.OpenWindow(WindowType.Empty);
        }

        private void CloseEmpty()
        {
            _uiManager.CloseWindow(WindowType.Empty);
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