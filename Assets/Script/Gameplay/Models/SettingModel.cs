using Script.Gameplay.Services;
using UnityEngine;

namespace Script.Gameplay
{
    public class SettingModel
    {
        private ProgressService _progressService;
        private StaticDataService _staticDataService;
        private const string MIXER_MUSIC = "Music";
        private const string MIXER_SFX = "SFX";

        public SettingModel(ProgressService progressService, StaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        public void Initialize()
        {
            SetMusicVolume(_progressService.PlayerProgress.MusicVolume);
            SetSFXVolume(_progressService.PlayerProgress.SFXVolume);
        }

        public void SetMusicVolume(float value)
        {
            var range = Mathf.Log10(value) * 40;
            _staticDataService.AudioData.Mixer.SetFloat(MIXER_MUSIC, range);
            _progressService.PlayerProgress.MusicVolume = value;
        }

        public void SetSFXVolume(float value)
        {
            var range = Mathf.Log10(value) * 40;
            _staticDataService.AudioData.Mixer.SetFloat(MIXER_SFX, range);
            _progressService.PlayerProgress.SFXVolume = value;
        }
        
        public bool MusicStatus()
        {
            return _progressService.PlayerProgress.MusicVolume >= 0f;
        }
        
        public bool SFXStatus()
        {
            return _progressService.PlayerProgress.SFXVolume >= 0f;
        }
    }
}