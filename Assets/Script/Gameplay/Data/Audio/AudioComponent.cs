using System;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [Serializable]
    public struct AudioComponent
    {
        public ClipID ID;
        public AudioClip Clip;
    }
}