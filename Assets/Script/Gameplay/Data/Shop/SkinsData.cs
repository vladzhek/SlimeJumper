using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "SkinsData", menuName = "Data/SkinsData")]
    public class SkinsData : ScriptableObject
    {
        public List<ShopComponent> Skins;
    }
}