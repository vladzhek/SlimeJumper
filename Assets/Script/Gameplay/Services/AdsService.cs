using System;
using UnityEngine;
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
            YandexSDK.YaSDK.onRewardedAdReward += UserGotReward;
        }

        public void ShowReward()
        {
            YandexSDK.YaSDK.instance.ShowRewarded("hard");
        }

        private void UserGotReward(string reward)
        {
            Debug.Log("GET REWARD");
        }
    }
}