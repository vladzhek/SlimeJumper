using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Data.Achievement
{
    [CreateAssetMenu(fileName = "AchievementData", menuName = "Data/AchievementData")]
    public class AchievementData : ScriptableObject
    {
        public List<AchieveComponent> Achievements;
    }
}