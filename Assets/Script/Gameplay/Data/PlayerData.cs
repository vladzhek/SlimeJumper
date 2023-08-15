using System;
using Script.Gameplay.Player;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public GameObject Prefab;
    }
}