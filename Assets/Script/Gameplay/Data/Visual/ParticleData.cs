using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "ParticleData", menuName = "Data/ParticleData")]
    public class ParticleData : ScriptableObject
    {
        public List<ParticleComponent> Particles;
    }
}