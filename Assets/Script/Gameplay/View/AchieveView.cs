using System;
using System.Collections.Generic;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using Script.Gameplay.Services;
using Script.Gameplay.View.SubView;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Gameplay.View
{
    public class AchieveView :  MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private RectTransform _content;
        [SerializeField] private AchieveSubView _achieveSubView;

        public event Action OnReload;

        private AchievementModel _achievementModel;
        private UIManager _uiManager;
        private TimeModel _timeModel;
        private CurrencyModel _currencyModel;
        private StaticDataService _staticDataService;

        private Dictionary<string, AchieveSubView> _subView = new();

        [Inject]
        public void Construct(AchievementModel achievementModel,
            UIManager uiManager,
            CurrencyModel currencyModel,
            StaticDataService staticDataService,
            ProgressService progressService)
        {
            _achievementModel = achievementModel;
            _uiManager = uiManager;
            _currencyModel = currencyModel;
            _staticDataService = staticDataService;
        }
        
        private void Awake()
        {
            InjectService.Instance.Inject(this);
            _menuButton.onClick.AddListener(GoToMenu);
            CreateSubViews();
        }

        private void CreateSubViews()
        {
            foreach (var (key, value ) in _achievementModel.GetAchievements())
            {
                var subView = Instantiate(_achieveSubView, _content);
                var sprite = GetSpriteCurrency(value.Data.RewardType);
                subView.Initialize(value, sprite);
                subView.OnButtonTake += ButtonTakeReward;
                _subView.Add(key, subView);
            }
        }

        private void ButtonTakeReward(string id)
        {
            _achievementModel.TakeReward(id);
            var achieve = _achievementModel.GetAchievements()[id];
            _currencyModel.AddCurrency(achieve.Data.RewardType, achieve.Data.RewardPrice);
        }
        
        private Sprite GetSpriteCurrency(CurrencyType currencyType)
        {
            switch (currencyType)
            {
                case CurrencyType.Soft:
                    return _staticDataService.Sprites[SpriteType.SoftCurrency].Sprite;
                case CurrencyType.Hard:
                    return _staticDataService.Sprites[SpriteType.HardCurrency].Sprite;
                default:
                    return _staticDataService.Sprites[SpriteType.Error].Sprite;;
            }
        }

        private void GoToMenu()
        {
            _uiManager.CloseWindow(WindowType.Achievement);
        }
    }
}