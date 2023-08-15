using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Gameplay.View.SubView
{
    public class AchieveSubView : MonoBehaviour
    {
        public event Action<string> OnButtonTake;
        
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _collectText;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _iconDone;
        [SerializeField] private Button _buttonTake;
        [SerializeField] private GameObject _rewardGroup;

        private Achievement _achievement;
        private Sprite _rewardSprite;

        public void Initialize(Achievement achievement, Sprite rewardSprite)
        {
            _achievement = achievement;
            _rewardSprite = rewardSprite;
            _buttonTake.onClick.AddListener(TakeReward);
            UpdateState();
        }

        private void UpdateState()
        {
            _title.text = _achievement.Data.title;
            _collectText.text = $"{_achievement.Progress.Collected}/{_achievement.Data.amount}";
            _slider.value = MapToNewRange(_achievement.Progress.Collected, 0, _achievement.Data.amount);
            _icon.sprite = _achievement.Data.Sprite;
            _iconDone.gameObject.SetActive(_achievement.Progress.IsTake);
            _buttonTake.gameObject.SetActive(_achievement.Progress.IsDone && !_achievement.Progress.IsTake);
            _rewardGroup.SetActive(!_achievement.Progress.IsTake);
            
            _rewardGroup.GetComponentInChildren<Image>().sprite = _rewardSprite;
            _rewardGroup.GetComponentInChildren<TMP_Text>().text = _achievement.Data.RewardPrice.ToString();
        }

        private void TakeReward()
        {
            _achievement.Progress.IsTake = true;
            OnButtonTake?.Invoke(_achievement.Data.ID);
            UpdateState();
        }

        private float MapToNewRange(float value, float originalMin, float originalMax)
        {
            float newMin = 0.1f;
            float newMax = 1.0f;
            
            if (value <= originalMin)
                return newMin;
            if (value >= originalMax)
                return newMax;
            
            float originalRange = originalMax - originalMin;
            float newRange = newMax - newMin;
            return (((value - originalMin) * newRange) / originalRange) + newMin;
        }
    }
}