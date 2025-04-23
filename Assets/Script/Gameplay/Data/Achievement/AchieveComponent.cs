using System;
using UnityEngine;

namespace Script.Gameplay.Data.Achievement
{
    [Serializable]
    public class AchieveComponent
    {
        public Sprite Sprite;
        public string ID;
        public AchieveType Type;
        public string title;
        public int amount;
        public CurrencyType RewardType;
        public int RewardPrice;
    }
}