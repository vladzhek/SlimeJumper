using System;
using System.Collections.Generic;
using Script.Gameplay.Data;
using Script.Gameplay.Data.Achievement;
using Script.Gameplay.Data.Progress;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;
using Zenject.SpaceFighter;

namespace Script.Gameplay
{
    public class AchievementModel
    {
        public event Action<string> OnAchieveProgress;
        
        private StaticDataService _staticDataService;
        private ProgressService _progressService;
        private CurrencyModel _currencyModel;
        private PlayerModel _playerModel;
        private Dictionary<string, Achievement> _achievements = new();

        private List<AchieveType> _currencyAchieve = new();

        public AchievementModel(StaticDataService staticDataService,
            CurrencyModel currencyModel,
            PlayerModel playerModel,
            ProgressService progressService)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _currencyModel = currencyModel;
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            TryCreateProgress();
            CreateOrLoadAchieves();
            _currencyModel.OnCollect += CollectCurrency;
            _currencyModel.OnSpend += SpendCurrency;
            _playerModel.OnDeath += PlayerDie;
            _playerModel.OnScoreUpdate += ScoreUpdate;
        }
        
        public Dictionary<string, Achievement> GetAchievements()
        {
            return _achievements;
        }
        
        public void TakeReward(string id)
        {
            var progress = _progressService.PlayerProgress.AchieveDataProgresses.Find(x => x.ID == id);
            progress.IsTake = _achievements[id].Progress.IsTake;
        }

        private void CreateOrLoadAchieves()
        {
            foreach (var (id, data) in _staticDataService.Achievements)
            {
                var progress = _progressService.PlayerProgress.AchieveDataProgresses
                    .Find(x => x.ID == id);
                var achieve = new Achievement(data, progress);
                _achievements.Add(id, achieve);
                
                achieve.OnDone += AchieveDone;
                achieve.OnProgress += AchieveProgress;
            }
            
            _currencyAchieve.Add(AchieveType.CollectSoft);
            _currencyAchieve.Add(AchieveType.SpendSoft);
        }

        private void AchieveProgress(string id)
        {
            var progress = _progressService.PlayerProgress.AchieveDataProgresses.Find(x => x.ID == id);
            progress.Collected = _achievements[id].Progress.Collected;
            
            OnAchieveProgress?.Invoke(id);
        }

        private void AchieveDone(string id)
        {
            var progress = _progressService.PlayerProgress.AchieveDataProgresses.Find(x => x.ID == id);
            progress.IsDone = _achievements[id].Progress.IsDone;
            
            OnAchieveProgress?.Invoke(id);
        }

        private void TryCreateProgress()
        {
            if (_progressService.PlayerProgress.AchieveDataProgresses.Count != 0)
            {
                return;
            }
            
            foreach (var (key,value) in _staticDataService.Achievements)
            {
                var data = new AchieveDataProgress(value.Type,false, 0, value.ID);
                _progressService.PlayerProgress.AchieveDataProgresses.Add(data);
            }
        }

        #region UpdateCollectProgress

        private void CollectCurrency(CurrencyType type, int amount)
        {
            switch(type)
            {
                case CurrencyType.Soft:
                    UpdateCollectProgress(amount, AchieveType.CollectSoft); break;
                case CurrencyType.Hard:
                    //UpdateCurrency(type, amount, AchieveType.CollectHard);
                    break;
            }
        }

        private void SpendCurrency(CurrencyType type, int amount)
        {
            switch(type)
            {
                case CurrencyType.Soft:
                    UpdateCollectProgress(amount, AchieveType.SpendSoft); 
                    break;
                case CurrencyType.Hard:
                    //UpdateCurrency(type, amount, AchieveType.CollectHard);
                    break;
            }
        }

        private void ScoreUpdate(int score)
        {
            UpdateCollectProgress(score, AchieveType.GetScore); 
        }

        private void PlayerDie()
        {
            UpdateCollectProgress(1, AchieveType.DieOnce); 
        }
        
        private void UpdateCollectProgress(int amount, AchieveType achieveType)
        {
            var dir = FindAchieveByType(achieveType).FindAll(x => x.Data.Type == achieveType);
            foreach (var value in dir)
            {
                value.UpdateProgress(amount);
            }
        }
        private List<Achievement> FindAchieveByType(AchieveType type)
        {
            List<Achievement> achieve = new();
            foreach (var (key, value) in _achievements)
            {
                if (value.Data.Type == type)
                {
                    achieve.Add(value);
                }
            }
            
            return achieve;
        }
        
        #endregion
    }
}