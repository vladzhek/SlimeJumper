using System;
using Script.Gameplay.Progress;
using YG;

namespace Script.Gameplay.Services
{
    public class ProgressService
    {
        private StaticDataService _staticDataService;
        public PlayerProgress PlayerProgress { get; private set; }
        public bool IsLoaded { get; set; } = false;
        public event Action OnLoaded;

        public ProgressService(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void InitializeProgress(PlayerProgress playerProgress)
        {
            PlayerProgress = playerProgress;

            IsLoaded = true;
            OnLoaded?.Invoke();
        }
        
        public void InitializeProgressYG()
        {
            PlayerProgress = YandexGame.savesData.PlayerProgress;

            IsLoaded = true;
            OnLoaded?.Invoke();
        }
    }
}