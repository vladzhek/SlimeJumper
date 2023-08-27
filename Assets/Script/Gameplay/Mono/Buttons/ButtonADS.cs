using Script.Gameplay.Services;
using Zenject;

namespace Script.Gameplay.Mono
{
    public class ButtonADS : ButtonBase
    {
        [Inject] private AdsService _adsService;
        public override void OnClick()
        {
            if(_adsService == null)
                InjectService.Instance.Inject(this);
            
            _adsService.ShowReward();
        }
    }
}