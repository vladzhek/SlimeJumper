using System.Collections.Generic;
using Script.Gameplay.Data;
using UnityEngine;

namespace Script.Gameplay.Services
{
    public class AudioService
    {
        private AudioSource _musicSource;
        private AudioSource _sfxSource;
        private StaticDataService _staticDataService;
        private Dictionary<AudioSourcesType, AudioSource> AudioSources = new();

        public void Initialize(StaticDataService staticDataService,
            AudioSource sfxSource,
            AudioSource musicSource)
        {
            _staticDataService = staticDataService;
            _musicSource = musicSource;
            _sfxSource = sfxSource;
            CreateAudioSource();
        }

        private void CreateAudioSource()
        {
            AudioSources.Add(AudioSourcesType.MusicSource, _musicSource);
            AudioSources.Add(AudioSourcesType.SFXSource, _sfxSource);
        }
        
        public void PlayAudio(AudioSourcesType idSource, ClipID clipID)
        {
            var clip = _staticDataService.AudioData.AudioClips.Find(x => x.ID == clipID).Clip;
            RandomPitchValue(AudioSources[idSource]);
            AudioSources[idSource].PlayOneShot(clip);
        }
        
        private void RandomPitchValue(AudioSource source)
        {
            source.pitch = Random.Range(0.92f, 1.1f);
        }
    }
}