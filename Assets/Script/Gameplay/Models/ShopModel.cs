using System;
using Script.Gameplay.Data;
using Script.Gameplay.Data.Progress;
using Script.Gameplay.Progress;
using Script.Gameplay.Services;

namespace Script.Gameplay
{
    public class ShopModel
    {
        public event Action<SkinType> OnUpdateSkin;
        
        private StaticDataService _staticDataService;
        private ProgressService _progressService;
        
        public ShopModel(StaticDataService staticDataService,
            ProgressService progressService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
        }
        
        public void Initialize()
        {
            if (_progressService.PlayerProgress.ShopDataProgresses.Count != 0)
            {
                return;
            }

            foreach (var (key,value) in _staticDataService.Skins)
            {
                var data = new ShopDataProgress(key,false, false);
                _progressService.PlayerProgress.ShopDataProgresses.Add(data);
            }

            var skinDefault = _progressService.PlayerProgress.ShopDataProgresses
                .Find(x => x.SkinID == SkinType.Default);
            skinDefault.IsBuyed = true;
            skinDefault.IsSelected = true;
            OnUpdateSkin?.Invoke(SkinType.Default);
        }

        public void SelectSkin(SkinType id)
        {
            var selectSkin = _progressService.PlayerProgress.ShopDataProgresses
                .Find(x => x.SkinID == id);
            
            if(!selectSkin.IsBuyed) return;
                
            foreach (var skin in _progressService.PlayerProgress.ShopDataProgresses)
            {
                skin.IsSelected = false;
            }

            selectSkin.IsSelected = true;
            OnUpdateSkin?.Invoke(id);
        }
    }
}