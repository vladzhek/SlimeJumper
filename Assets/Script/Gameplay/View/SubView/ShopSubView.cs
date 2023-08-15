using System;
using Script.Gameplay.Data;
using Script.Gameplay.Data.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Gameplay.View.SubView
{
    public class ShopSubView : MonoBehaviour
    {
        public event Action<SkinType> OnBuy;
        public event Action<SkinType> OnSelect;
        
        [SerializeField] private Button _selectedTarget;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Image _buttonCurrency;
        [SerializeField] private Image _skin;
        [SerializeField] private Image _selectIcon;

        private ShopComponent _component;
        private ShopDataProgress _progressData;
        private Sprite _currencySprite;
        public void Initialize(ShopComponent component, ShopDataProgress progressData, Sprite currencySprite)
        {
            _component = component;
            _progressData = progressData;
            _currencySprite = currencySprite;
            
            _button.onClick.AddListener(ButtonBuy);
            _selectedTarget.onClick.AddListener(ButtonSelect);

            UpdateState();
        }

        private void ButtonSelect()
        {
            OnSelect?.Invoke(_component.SkinType);
        }

        private void ButtonBuy()
        {
            OnBuy?.Invoke(_component.SkinType);
        }

        public void UpdateState()
        {
            _skin.sprite = _component.Skin;
            _buttonText.text = _component.Price.ToString();
            _buttonCurrency.sprite = _currencySprite;
            _button.gameObject.SetActive(!_progressData.IsBuyed);
            _selectIcon.gameObject.SetActive(_progressData.IsSelected);
        }
    }
}