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
#if UNITY_ANDROID
            PlayerPrefs.SetString(SavesKey, JsonUtility.ToJson(_progressService.PlayerProgress));
            Debug.Log("[Save] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
#endif
        }
        
        public void LoadProgress()
        {
#if UNITY_ANDROID
            Debug.Log("[Load2] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
            _progressService.InitializeProgress(GetOrCreate());
#endif
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