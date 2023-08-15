using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "SpriteData", menuName = "Data/SpriteData")]
    public class SpriteData : ScriptableObject
    {
        public List<SpriteComponent> Sprites;
    }
}