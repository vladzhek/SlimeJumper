using System.Collections.Generic;
using Script.Gameplay.Data;
using Script.Gameplay.Data.Achievement;
using UnityEngine;

namespace Script.Gameplay.Services
{
    public class StaticDataService
    {
        public PlayerData PlayerData;
        public SpawnData SpawnData;
        public AudioData AudioData;
        public MaterialsData MaterialData;
        public ParticleData ParticleData;
        public Dictionary<WindowType, WindowData> Windows = new();
        public Dictionary<SkinType, ShopComponent> Skins = new();
        public Dictionary<SpriteType, SpriteComponent> Sprites = new();
        public Dictionary<string, AchieveComponent> Achievements = new();
        

        public void Load()
        {
            LoadPrefabs();
            LoadWindows();
            LoadShop();
            LoadAchievements();
        }

        private void LoadPrefabs()
        {
            PlayerData = Resources.Load<PlayerData>("Configs/PlayerData");
            SpawnData = Resources.Load<SpawnData>("Configs/SpawnData");
            AudioData = Resources.Load<AudioData>("Configs/AudioData");
            MaterialData = Resources.Load<MaterialsData>("Configs/Visual/MaterialsData");
            ParticleData = Resources.Load<ParticleData>("Configs/Visual/ParticleData");
        }
        
        private void LoadWindows()
        {
            var data = Resources.Load<WindowsData>("Configs/WindowsData");
            foreach (var window in data.Windows)
            {
                Windows.Add(window.Type, window);
            }
        }
        
        private void LoadShop()
        {
            var data = Resources.Load<SkinsData>("Configs/Skins/SkinsData");
            var sprites = Resources.Load<SpriteData>("Configs/Skins/SpriteData");

            foreach (var skin in data.Skins)
            {
                Skins.Add(skin.SkinType, skin);
            }
            
            foreach (var sprite in sprites.Sprites)
            {
                Sprites.Add(sprite.ID, sprite);
            }
        }

        private void LoadAchievements()
        {
            var achievements = Resources.Load<AchievementData>("Configs/AchievementData");
            foreach (var achieve in achievements.Achievements)
            {
                Achievements.Add(achieve.ID, achieve);
            }
        }
    }
}