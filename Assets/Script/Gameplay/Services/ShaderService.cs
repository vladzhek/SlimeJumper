using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Services
{
    public class ShaderService
    {
        private StaticDataService _staticDataService;
        private PlayerModel _playerModel;
        
        private Dictionary<Material, float> _materials = new();
        
        private readonly int Speed = Shader.PropertyToID("_Speed");

        public ShaderService(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Initialize(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            foreach (var material in _staticDataService.MaterialData.Materials)
            {
                var defaultValue = material.Material.GetFloat(Speed);
                _materials.Add(material.Material, defaultValue);
            }
        }

        public void ActivePlayerDeathAnim(bool isActive)
        {
            _playerModel.AnimControl.ActiveAnimation(isActive);
        }

        public void PauseDecorSpeed(bool isActive)
        {
            foreach (var (material, valueDef) in _materials)
            {
                var value = isActive ? 0f : valueDef;
                material.SetFloat(Speed ,value);
            }
        }
    }
}