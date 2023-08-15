using System;
using Script.Gameplay.Data.Achievement;

namespace Script.Gameplay.Data.Progress
{
    [Serializable]
    public class AchieveDataProgress
    {
        public string ID;
        public AchieveType Type;
        public bool IsDone;
        public bool IsTake;
        public int Collected;

        public AchieveDataProgress(AchieveType type, bool isDone, int collected, string id)
        {
            Type = type;
            IsDone = isDone;
            Collected = collected;
            ID = id;
        }
    }
}