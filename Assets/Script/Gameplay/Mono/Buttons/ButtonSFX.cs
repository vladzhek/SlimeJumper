using Script.Gameplay.Data;
using Script.Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Script.Gameplay.Mono
{
    public class ButtonSFX : ButtonBase
    {
        [SerializeField] private ClipID SFX;

        [Inject] private AudioService _audioService;

        public override void OnClick()
        {
            if(_audioService == null)
                InjectService.Instance.Inject(this);
            
            _audioService.PlayAudio(AudioSourcesType.SFXSource, SFX);
        }
    }
}