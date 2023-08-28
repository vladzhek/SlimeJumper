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
            // Mobile device
            //PlayerPrefs.SetString(SavesKey, JsonUtility.ToJson(_progressService.PlayerProgress));
            //Debug.Log("[Save] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
            
            // Yandex Games 
            YandexGame.savesData.PlayerProgress = _progressService.PlayerProgress;
            YandexGame.SaveProgress();
        }
        
        public void LoadProgress()
        {
            //Debug.Log("[Load2] \n" + JsonUtility.ToJson(_progressService.PlayerProgress));
            //_progressService.InitializeProgress(GetOrCreate());
            
            // Yandex Games 
            
            YandexGame.LoadProgress();
            _progressService.InitializeProgressYG();
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