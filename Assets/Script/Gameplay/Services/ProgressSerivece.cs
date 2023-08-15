using System;
using Script.Gameplay.Progress;

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
    }
}