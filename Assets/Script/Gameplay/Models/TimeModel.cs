using Script.Gameplay.Player;
using UnityEngine;

namespace Script.Gameplay
{
    public class TimeModel
    {
        public bool Pause { get; private set;}

        public void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }

        public void SetActivePause(bool isPause)
        {
            Pause = isPause;
            Time.timeScale = Pause ? 0 : 1;
        }
    }
}