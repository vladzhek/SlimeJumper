using System;
using System.Collections.Generic;
using System.Linq;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;
using Script.Gameplay.View.SubView;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Gameplay.View
{
    public class ShopView : MonoBehaviour
    {
        private event Action OnUpdateState;
        private const int COUNT_COLUMNS = 4;
        
        [SerializeField] private Button _closeWindow;
        [SerializeField] private ShopSubView _shopSubView;
        [SerializeField] private GameObject _container;

        private PlayerModel _playerModel;
        private UIManager _uiManager;
        private StaticDataService _staticDataService;
        private ProgressService _progress;
        private CurrencyModel _currencyModel;
        private ShopModel _shopModel;
        
        private Dictionary<SkinType, ShopSubView> _subView = new();

        [Inject]
        public void Construct(PlayerModel playerModel,
            UIManager uiManager,
            StaticDataService staticDataService,
            ProgressService progress,
            CurrencyModel currencyModel,
            ShopModel shopModel,
            SpawnerModel spawnerModel)
        {
            _playerModel = playerModel;
            _uiManager = uiManager;
            _shopModel = shopModel;
            _currencyModel = currencyModel;
            _staticDataService = staticDataService;
            _progress = progress;
        }
        
        private void Awake()
        {
            InjectService.Instance.Inject(this);
            
            _closeWindow.onClick.AddListener(CloseWindow);
            CreateSubViews();
        }

        private void Start()
        {
            var width = _container.GetComponent<RectTransform>().rect.width;
            var cellSize = new Vector2(width / COUNT_COLUMNS, width / COUNT_COLUMNS);
            _container.GetComponent<GridLayoutGroup>().cellSize = cellSize;
            _container.GetComponent<GridLayoutGroup>().spacing = cellSize / 2;
        }

        private void CreateSubViews()
        {
            var sortedSkins = SortingSkinsByPrice(_staticDataService.Skins);
            foreach (var (key, value ) in sortedSkins)
            {
                var subView = Instantiate(_shopSubView, _container.GetComponent<RectTransform>());
                var progress = _progress.PlayerProgress.ShopDataProgresses.Find(x => x.SkinID == key);
                var sprite = GetSpriteCurrency(value.CurrencyID);
                
                subView.Initialize(value, progress, sprite);
                subView.OnBuy += ButtonBuy;
                subView.OnSelect += ButtonSelect;
                OnUpdateState += subView.UpdateState;
                _subView.Add(key, subView);
            }
        }

        private void ButtonSelect(SkinType id)
        {
            _shopModel.SelectSkin(id);
            OnUpdateState?.Invoke();
        }

        private void ButtonBuy(SkinType id)
        {
            var staticData = _staticDataService.Skins[id];
            var progressData = _progress.PlayerProgress.ShopDataProgresses.Find(x => x.SkinID == id);

            if (!_currencyModel.SpendCurrency(staticData.CurrencyID, staticData.Price)) return;
            
            progressData.IsBuyed = true;
            _shopModel.SelectSkin(id);
            OnUpdateState?.Invoke();
        }

        private void CloseWindow()
        {
            _uiManager.CloseWindow(WindowType.Shop);
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

        private Dictionary<SkinType, ShopComponent> SortingSkinsByPrice(Dictionary<SkinType, ShopComponent> skins)
        {
            var currencyOrder = new List<CurrencyType>
            {
                CurrencyType.Soft,
                CurrencyType.Hard,
            };

            Dictionary<SkinType, ShopComponent> sortedSkins = new();
            
            foreach (var currencyType in currencyOrder)
            {
                var skinsWithCurrency = skins.Where(kvp => kvp.Value.CurrencyID == currencyType);
                var sortedSkinsWithCurrency = skinsWithCurrency.OrderBy(kvp => kvp.Value.Price);
                
                foreach (var kvp in sortedSkinsWithCurrency)
                {
                    sortedSkins.Add(kvp.Key, kvp.Value);
                }
            }
            return sortedSkins;
        }
    }
}