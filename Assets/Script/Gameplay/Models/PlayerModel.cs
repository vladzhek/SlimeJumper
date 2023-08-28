using System;
using Script.Gameplay.Data;
using Script.Gameplay.Player;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Gameplay
{
    public class PlayerModel
    {
        public event Action OnStartJump;
        public event Action OnEndJump;
        public event Action<float> OnTickInJump;
        public event Action OnDeath;
        public event Action<int> OnScoreUpdate;

        public int TotalScore { get; private set; }
        private bool _isCanControlled;
        private StaticDataService _staticDataService;
        private PlayerJump _playerJump;
        private CurrencyModel _currencyModel;
        private PlayerController _playerController;
        private ProgressService _progressService;
        private ShopModel _shopModel;
        private SpawnerModel _spawnerModel;
        
        private Vector3 _spawnPos;

        public PlayerModel(StaticDataService staticDataService,
            PlayerJump playerJump, 
            ShopModel shopModel, 
            ProgressService progressService, 
            SpawnerModel spawnerModel,
            CurrencyModel currencyModel)
        {
            _staticDataService = staticDataService;
            _playerJump = playerJump;
            _currencyModel = currencyModel;
            _progressService = progressService;
            _shopModel = shopModel;
            _spawnerModel = spawnerModel;
            Subscribe();
        }

        public void Initialize(Vector3 spawnPosition)
        {
            TotalScore = 0;
            _isCanControlled = false;
            _spawnPos = spawnPosition;
            
            var prefab = GameObject.Instantiate(_staticDataService.PlayerData.Prefab, _spawnPos, Quaternion.identity);
            
            _playerController = prefab.GetComponent<PlayerController>();
            _playerJump.Initialize(_playerController.Rigidbody);
            _playerController.OnCollisionDeath += CollisionDeath;
            _playerController.OnCollisionWay += CollisionWay;
            _playerController.OnCollisionPlatform += _playerJump.OnGround;
            _playerController.OnCollisionBonus += CollisionBonus;
            _shopModel.OnUpdateSkin += UpdateSkin;
            
            var currentSkin = _progressService.PlayerProgress.ShopDataProgresses
                .Find(x => x.IsSelected);
            UpdateSkin(currentSkin.SkinID);
        }
        
        private void Subscribe()
        {
            OnStartJump += _playerJump.StartJump;
            OnEndJump += _playerJump.EndJump;
            _playerJump.IsTick += TickOnJump;
        }

        public void SetPlayerControlled(bool isActive)
        {
            _isCanControlled = isActive;
            _playerController.Rigidbody.bodyType = isActive  ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
        }

        public void ReloadPlayer()
        {
            _playerController.transform.position = _spawnPos;
        }

        private void CollisionBonus(BonusType type)
        {
            switch (type)
            {
                case BonusType.Coin:
                    _currencyModel.AddCurrency(CurrencyType.Soft, 1);
                    break;
                case BonusType.HardCoin:
                    _currencyModel.AddCurrency(CurrencyType.Hard, 1);
                    break;
                default:
                    Debug.LogWarning("[PlayerModel] CollisionBonus() => switch"); break;
            }
        }

        private void CollisionWay()
        {
            TotalScore++;
            OnScoreUpdate?.Invoke(TotalScore);
            _spawnerModel.IncrementCD += 0.1f;
        }

        private void CollisionDeath()
        {
            SetPlayerControlled(false);
            if (_progressService.PlayerProgress.BestScore < TotalScore)
                _progressService.PlayerProgress.BestScore = TotalScore;
            
            OnDeath?.Invoke();
            
        }

        private void TickOnJump(float tick)
        {
            OnTickInJump?.Invoke(tick);
        }

        public void StartJump()
        {
            if(_isCanControlled)
                OnStartJump?.Invoke();
        }

        public void EndJump()
        {
            if(_isCanControlled)
                OnEndJump?.Invoke(); 
        }

        private void UpdateSkin(SkinType skinID)
        {
            var skin = _staticDataService.Skins[skinID].Skin;
            _playerController.UpdateSkin(skin);
        }

        public void ResetScore()
        {
            TotalScore = 0;
        }
    }
}