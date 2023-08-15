using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "AudioData", menuName = "Data/AudioData")]
    public class AudioData : ScriptableObject
    {
        public AudioMixer Mixer;
        public List<AudioComponent> AudioClips;
    }
}