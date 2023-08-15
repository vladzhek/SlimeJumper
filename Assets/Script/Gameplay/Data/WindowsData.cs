using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Data
{
    [CreateAssetMenu(fileName = "WindowsData", menuName = "Data/WindowsData")]
    public class WindowsData : ScriptableObject
    {
        public List<WindowData> Windows;
    }
}