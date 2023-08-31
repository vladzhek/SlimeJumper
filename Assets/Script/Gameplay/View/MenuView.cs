using System;
using Script.Gameplay.Data;
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

        private PlayerModel _playerModel;
        private UIManager _uiManager;
        private SpawnerModel _spawnerModel;
        private ShaderService _shaderService;

        [Inject]
        public void Construct(PlayerModel playerModel,
            UIManager uiManager,
            ShaderService shaderService,
            SpawnerModel spawnerModel)
        {
            _playerModel = playerModel;
            _uiManager = uiManager;
            _spawnerModel = spawnerModel;
            _shaderService = shaderService;
        }
        
        private void Awake()
        {
            InjectService.Instance.Inject(this);
            
            _startGameButton.onClick.AddListener(StartGame);
            _shopButton.onClick.AddListener(Shop);
            _AchieveButton.onClick.AddListener(Achieve);
        }

        private void Achieve()
        {
            _uiManager.OpenWindow(WindowType.Achievement);
        }

        private void Shop()
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