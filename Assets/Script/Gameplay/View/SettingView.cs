using System;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using Script.Gameplay.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Script.Gameplay.View
{
    public class SettingView : MonoBehaviour
    {
        [SerializeField] private Button _closeWindow;
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderSFX;

        private PlayerModel _playerModel;
        private UIManager _uiManager;
        private TimeModel _timeModel;
        private SettingModel _settingModel;
        private ProgressService _progressService;

        [Inject]
        public void Construct(PlayerModel playerModel,
            UIManager uiManager,
            TimeModel timeModel,
            SettingModel settingModel,
            ProgressService progressService,
            SpawnerModel spawnerModel)
        {
            _playerModel = playerModel;
            _uiManager = uiManager;
            _timeModel = timeModel;
            _settingModel = settingModel;
            _progressService = progressService;
        }
        
        private void Awake()
        {
            InjectService.Instance.Inject(this);
            
            _timeModel.SetActivePause(true);
            _closeWindow.onClick.AddListener(CloseWindow);
            
            
            _sliderMusic.onValueChanged.AddListener(SliderChangeMusic);
            _sliderMusic.value = _progressService.PlayerProgress.MusicVolume;
            _sliderSFX.onValueChanged.AddListener(SliderChangeSFX);
            _sliderSFX.value = _progressService.PlayerProgress.SFXVolume;
        }

        private void SliderChangeMusic(float value)
        {
            _settingModel.SetMusicVolume(value);
        }
        
        private void SliderChangeSFX(float value)
        {
            _settingModel.SetSFXVolume(value);
        }

        private void CloseWindow()
        {
            _uiManager.CloseWindow(WindowType.Setting);
            _timeModel.SetActivePause(false);
        }
    }
}