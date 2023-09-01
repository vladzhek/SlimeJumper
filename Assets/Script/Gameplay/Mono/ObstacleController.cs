using System;
using Script.Gameplay.Data;
using UnityEngine;

namespace Script.Gameplay.Mono
{
    public class ObstacleController : MonoBehaviour
    {
        private bool isCanMove;
        public event Action<GameObject> OnDestroy;

        public void Start()
        {
            isCanMove = true;
        }

        public void Update()
        {
            if(!isCanMove) return;
            
            var pos = transform.position;
            var movePos = pos.x - Values.PLATFORM_START_SPEED * Time.deltaTime;
            transform.position = new Vector3(movePos, pos.y, pos.z);
        }

        public void CanMove(bool isCan)
        {
            isCanMove = isCan;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Values.CLEAR_OBS_COLLIDER))
            {
                OnDestroy?.Invoke(gameObject);
            }
        }
    }
}