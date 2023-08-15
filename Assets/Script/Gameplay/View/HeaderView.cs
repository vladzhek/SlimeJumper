using System;
using Script.Gameplay.Mono;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Gameplay.Data.Game
{
    public class HeaderView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coin;
        [SerializeField] private TMP_Text _rubin;
        [SerializeField] private Button _setting;
        
        private CurrencyModel _currencyModel;
        private ProgressService _progressService;
        private UIManager _uiManager;
        private TimeModel _timeModel;
        
        [Inject]
        public void Construct(CurrencyModel currencyModel,
            UIManager uiManager,
            TimeModel timeModel,
            ProgressService progressService)
        {
            _currencyModel = currencyModel;
            _progressService = progressService;
            _uiManager = uiManager;
            _timeModel = timeModel;
            _currencyModel.OnCurrencyUpdate += UpdateCurrency;
        }
        
        private void Awake()
        {
            InjectService.Instance.Inject(this);
            UpdateState();
            _setting.onClick.AddListener(OpenSetting);
        }

        private void OpenSetting()
        {
            _uiManager.OpenWindow(WindowType.Setting);
        }

        private void UpdateState()
        {
            _coin.text = _currencyModel.GetCurrency(CurrencyType.Soft).ToString();
            _rubin.text = _currencyModel.GetCurrency(CurrencyType.Hard).ToString();
        }

        private void UpdateCurrency(CurrencyType type,int amount)
        {
            switch (type)
            {
                case CurrencyType.Soft:
                    _coin.text = amount.ToString();
                    break;
                case CurrencyType.Hard:
                    _rubin.text = amount.ToString();
                    break;
            }
        }
    }
}