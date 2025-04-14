using System;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using Script.Gameplay.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.Gameplay.View
{
    public class DeathView : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text _bestScore;
        [SerializeField] private TMP_Text _score;

        public event Action OnReload; 

        private PlayerModel _playerModel;
        private UIManager _uiManager;
        private TimeModel _timeModel;
        private ProgressService _progressService;
        

        [Inject]
        public void Construct(PlayerModel playerModel,
            UIManager uiManager,
            TimeModel timeModel,
            ProgressService progressService)
        {
            _playerModel = playerModel;
            _uiManager = uiManager;
            _timeModel = timeModel;
            _progressService = progressService;
        }

        private void Awake()
        {
            InjectService.Instance.Inject(this);
            
            _bestScore.text = _progressService.PlayerProgress.BestScore.ToString();
            _score.text = _playerModel.TotalScore.ToString();
            _menuButton.onClick.AddListener(GoToMenu);
        }
        
        private void GoToMenu()
        {
            _uiManager.CloseWindow(WindowType.Death);
            _uiManager.ReloadUI();
        }
    }
}