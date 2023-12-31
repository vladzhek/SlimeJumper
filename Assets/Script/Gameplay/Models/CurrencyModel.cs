﻿using System;
using System.Collections.Generic;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;

namespace Script.Gameplay
{
    public class CurrencyModel
    {
        public event Action<CurrencyType, int> OnCurrencyUpdate;
        public event Action<CurrencyType, int> OnSpend;
        public event Action<CurrencyType, int> OnCollect;
        private ProgressService _progressService;
        private SaveLoadService _saveLoadService;
        
        private int _coins;
        private Dictionary<CurrencyType, int> _currency = new();

        public CurrencyModel(ProgressService progressService, SaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void AddCurrency(CurrencyType type, int amount)
        {
            var currency = _progressService.PlayerProgress.CurrencyProgress
                .Find(x => x.Type == type);
            
            currency.Amount += amount;
            OnCurrencyUpdate?.Invoke(type, currency.Amount);
            OnCollect?.Invoke(type, amount);
            _saveLoadService.SaveProgress();
        }

        public bool SpendCurrency(CurrencyType type, int amount)
        {
            var currency = _progressService.PlayerProgress.CurrencyProgress.Find(x => x.Type == type);

            if (amount > currency.Amount)
            {
                return false;
            }

            currency.Amount -= amount;
            OnCurrencyUpdate?.Invoke(type, currency.Amount);
            OnSpend?.Invoke(type, amount);
            _saveLoadService.SaveProgress();
            return true;
        }

        public int GetCurrency(CurrencyType type)
        {
            return _progressService.PlayerProgress.CurrencyProgress
                .Find(x => x.Type == type).Amount;
        }
    }
}