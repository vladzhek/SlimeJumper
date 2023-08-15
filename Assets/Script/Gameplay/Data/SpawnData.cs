using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "Data/SpawnData")]
    public class SpawnData : ScriptableObject
    {
        public float Cooldown;
        public List<ObstacleComponent> Obstacles;
        
    }
}