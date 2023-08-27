using System.Collections.Generic;
using Script.Gameplay.Data;
using Script.Gameplay.Mono;
using Script.Gameplay.Services;
using UnityEngine;
using Zenject;

namespace Script.Gameplay
{
    public class SpawnerModel
    {        
        private Transform _spawnPoint;
        private StaticDataService _staticDataService;
        private Dictionary<GameObject, string> _obstacles = new();
        
        private bool _spawnStatus;
        private float _cooldown = 0f;
        private SpawnData _spawnData;

        public float IncrementCD { get; set; }

        [Inject]
        public void Construct(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Initialize(Transform spawnPoint)
        {
            IncrementCD = 0;
            _spawnPoint = spawnPoint;
            _spawnData = _staticDataService.SpawnData;
        }

        public void SetSpawnStatus(bool isActive)
        {
            _spawnStatus = isActive;
        }

        public void SetActiveMovement(bool isActive)
        {
            foreach (var (key, value) in _obstacles)
            {
                key.GetComponent<ObstacleController>().CanMove(isActive);
            }
        }

        public void SpawnObstacle()
        {
            if(!_spawnStatus) return;
            if (_cooldown > 0)
            {
                _cooldown -= 1 * Time.deltaTime;
            }
            else
            {
                var index = Random.Range(0, _spawnData.Obstacles.Count);
                var currObj = _spawnData.Obstacles[index];
                var prefab = GameObject.Instantiate(currObj.Prefab, _spawnPoint.position, Quaternion.identity);
                prefab.GetComponent<ObstacleController>().OnDestroy += DestroyObs;
                _obstacles.Add(prefab, currObj.ID);

                if (IncrementCD > 1.5f) IncrementCD = 1.5f;

                _cooldown = _spawnData.Cooldown - IncrementCD;
            }
        }

        public void ClearObstacle()
        {
            foreach (var (key, value) in _obstacles)
            {
                GameObject.Destroy(key);
            }
            _obstacles.Clear();
            IncrementCD = 0;
        }

        private void DestroyObs(GameObject gameObj)
        {
            GameObject.Destroy(gameObj);
            _obstacles.Remove(gameObj);
        }
        
        public bool GetSpawnStatus()
        {
            return _spawnStatus;
        }
    }
}
