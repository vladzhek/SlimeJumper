using System;

namespace Script.Gameplay.Data.Progress
{
    [Serializable]
    public class ShopDataProgress
    {
        public SkinType SkinID;
        public bool IsBuyed;
        public bool IsSelected;

        public ShopDataProgress(SkinType skinID, bool isBuyed, bool isSelected)
        {
            SkinID = skinID;
            IsBuyed = isBuyed;
            IsSelected = isSelected;
        }
    }
}