using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "Data/WindowData")]
    public class WindowData : ScriptableObject
    {
        public GameObject Prefab;
        public WindowType Type;
    }
}