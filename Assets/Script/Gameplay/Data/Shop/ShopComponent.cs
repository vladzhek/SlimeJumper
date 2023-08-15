using System;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [Serializable]
    public class ShopComponent
    {
        public SkinType SkinType;
        public Sprite Skin;
        public CurrencyType CurrencyID;
        public int Price;
    }
}