using System;
using Script.Gameplay.Progress;
using UnityEngine;
using YG;

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
#if UNITY_EDITOR || UNITY_WEBGL
            //ResetData();
            YandexGame.savesData.PlayerProgress = _progressService.PlayerProgress;
            YandexGame.SaveProgress();
#endif            
        }
        
        public void LoadProgress()
        {
#if UNITY_ANDROID
            Debug.Log("[Load2] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
            _progressService.InitializeProgress(GetOrCreate());
#endif
            
#if UNITY_EDITOR || UNITY_WEBGL
            YandexGame.LoadProgress();
            _progressService.InitializeProgressYG();
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

        private void ResetData()
        {
            YandexGame.savesData.PlayerProgress = new PlayerProgress();
        }
    }
}