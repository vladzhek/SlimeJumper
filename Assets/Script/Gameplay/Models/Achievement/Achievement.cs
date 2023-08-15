using System;
using Script.Gameplay.Data.Achievement;
using Script.Gameplay.Data.Progress;
using UnityEngine;

namespace Script.Gameplay
{
    public class Achievement
    {
        public event Action<string> OnProgress;
        public event Action<string> OnDone;
        
        public AchieveComponent Data { get; private set; }
        public AchieveDataProgress Progress { get; private set; }

        public Achievement(AchieveComponent data, AchieveDataProgress progress)
        {
            Data = data;
            Progress = progress;
        }

        public void UpdateProgress(int amount)
        {
            if (Progress.Collected + amount >= Data.amount)
            {
                Progress.Collected = Data.amount;
                Progress.IsDone = true;
                OnDone?.Invoke(Data.ID);
            }
            else
            {
                Progress.Collected += amount;
                OnProgress?.Invoke(Data.ID);
            }
        }
    }
}