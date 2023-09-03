using System;
using Script.Gameplay.Data;
using Script.Gameplay.Data.Achievement;
using Script.Gameplay.Mono;
using Script.Gameplay.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Gameplay.View
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _AchieveButton;
        [SerializeField] private GameObject _notifIcon;
        
        private PlayerModel _playerModel;
        private UIManager _uiManager;
        private AchievementModel _achievementModel;
        private SpawnerModel _spawnerModel;
        private ShaderService _shaderService;

        [Inject]
        public void Construct(PlayerModel playerModel,
            UIManager uiManager,
            ShaderService shaderService,
            AchievementModel achievementModel,
            SpawnerModel spawnerModel)
        {
            _playerModel = playerModel;
            _uiManager = uiManager;
            _spawnerModel = spawnerModel;
            _shaderService = shaderService;
            _achievementModel = achievementModel;
        }
        
        private void Awake()
        {
            InjectService.Instance.Inject(this);
            
            _startGameButton.onClick.AddListener(StartGame);
            _shopButton.onClick.AddListener(ShopButton);
            _AchieveButton.onClick.AddListener(AchieveButton);
        }

        private void OnEnable()
        {
            Subscribe();
            _notifIcon.SetActive(_achievementModel.CanTakeAchive);
        }

        private void Subscribe()
        {
            _achievementModel.OnAchieveDone += Notification;
        }

        private void Notification(NotifType achieveType)
        {
            if(_notifIcon == null) return;
            
            switch (achieveType)
            {
                case NotifType.Achievement:
                    _notifIcon.SetActive(true);
                    break;
            }
        }

        private void AchieveButton()
        {
            _uiManager.OpenWindow(WindowType.Achievement);
            _notifIcon.SetActive(false);
            _achievementModel.CanTakeAchive = false;
        }

        private void ShopButton()
        {
            _uiManager.OpenWindow(WindowType.Shop);
        }

        private void StartGame()
        {
            _playerModel.ResetScore();
            _uiManager.CloseWindow(WindowType.Menu);
            _uiManager.OpenWindow(WindowType.Gameplay);
            _playerModel.SetPlayerControlled(true);
            _spawnerModel.SetSpawnStatus(true);
            _shaderService.PauseDecorSpeed(false);
        }
    }
}