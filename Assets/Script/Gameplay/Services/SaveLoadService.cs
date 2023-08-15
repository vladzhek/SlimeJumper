using Script.Gameplay.Progress;
using UnityEngine;

namespace Script.Gameplay.Services
{
    public class SaveLoadService
    {
        private const string SavesKey = "Saves";
        private ProgressService _progressService;
        
        public SaveLoadService(ProgressService progressService)
        {
            _progressService = progressService;
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(SavesKey, JsonUtility.ToJson(_progressService.PlayerProgress));
            Debug.Log("[Save] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
        }
        
        public void LoadProgress()
        {
            _progressService.InitializeProgress(GetOrCreate());
            Debug.Log("[Load2] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
        }
        
        private PlayerProgress GetOrCreate()
        {
            if(PlayerPrefs.HasKey(SavesKey))
            {
                var saves = PlayerPrefs.GetString(SavesKey);
                return JsonUtility.FromJson<PlayerProgress>(saves);
            }
            
            return new PlayerProgress();
        }
    }
}