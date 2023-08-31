using System;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [Serializable]
    public struct ParticleComponent
    {
        public ParticleType ID;
        public ParticleSystem Particle;
    }
}