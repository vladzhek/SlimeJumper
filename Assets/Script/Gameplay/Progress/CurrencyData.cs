using System;

namespace Script.Gameplay.Progress
{
    [Serializable]
    public class CurrencyData
    {
        public CurrencyType Type;
        public int Amount;

        public CurrencyData(CurrencyType type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}