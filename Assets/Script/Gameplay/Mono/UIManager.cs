using System;
using System.Collections.Generic;
using Script.Gameplay.Data;
using Script.Gameplay.Data.Game;
using Script.Gameplay.Services;
using Script.Gameplay.View;
using UnityEngine;
using Zenject;

namespace Script.Gameplay.Mono
{
    public class UIManager : MonoBehaviour
    {
        public event Action OnReload;
        
        [SerializeField] private RectTransform _parent;
        
        private Dictionary<WindowType, GameObject> _openWindows;
        
        private StaticDataService _staticDataService;
        private DiContainer _container;

        [Inject]
        public void Construct(StaticDataService staticDataService, DiContainer container)
        {
            _staticDataService = staticDataService;
            _container = container;
        }

        public void Load()
        {
            _openWindows = new();
        }

        public void OpenWindow(WindowType type)
        {
            if(_openWindows.ContainsKey(type)) return;
            
            var prefab = Instantiate(_staticDataService.Windows[type].Prefab, _parent);
            _openWindows.Add(type, prefab);
        }
        
        public void CloseWindow(WindowType type)
        {
            if(!_openWindows.ContainsKey(type)) return;

            Destroy(_openWindows[type]);
            _openWindows.Remove(type);
        }

        private void CloseAllWindows()
        {
            foreach (var (key, value) in _openWindows)
            {
                Destroy(_openWindows[key]);
            }
            _openWindows.Clear();
        }

        public void ReloadUI()
        {
            OnReload?.Invoke();
            CloseAllWindows();
            OpenWindow(WindowType.Menu);
            OpenWindow(WindowType.Header);
        }
    }
}