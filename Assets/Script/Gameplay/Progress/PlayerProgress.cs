using System;
using System.Collections.Generic;
using Script.Gameplay.Data.Progress;

namespace Script.Gameplay.Progress
{
    [Serializable]
    public class PlayerProgress
    {
        public List<CurrencyData> CurrencyProgress = new();
        public List<ShopDataProgress> ShopDataProgresses = new();
        public List<AchieveDataProgress> AchieveDataProgresses = new();
        
        public int BestScore;
        public float SFXVolume;
        public float MusicVolume;

        public PlayerProgress()
        {
            CurrencyProgress.Add(new CurrencyData(CurrencyType.Soft, 0));
            CurrencyProgress.Add(new CurrencyData(CurrencyType.Hard, 0));
            MusicVolume = 1;
            SFXVolume = 1;
        }
    }
}