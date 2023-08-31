using System;
using Script.Gameplay.Data;
using UnityEngine;

namespace Script.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public AnimationController AnimControl;
        [SerializeField] private SpriteRenderer _playerSkin;

        public event Action OnCollisionDeath;
        public event Action OnCollisionPlatform;
        public event Action OnCollisionWay;
        public event Action<BonusType> OnCollisionBonus;
        public Rigidbody2D Rigidbody => GetComponent<Rigidbody2D>();
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag(Values.DEATH_COLLIDER))
            {
                OnCollisionDeath?.Invoke();
            }
            if (collision.collider.CompareTag(Values.PLATFORM_COLLIDER))
            {
                OnCollisionPlatform?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Values.WAY_COLLIDER))
            {
                OnCollisionWay?.Invoke();
            }
            if (collision.CompareTag(Values.BONUS_COLLIDER))
            {
                var bonusType = collision.GetComponent<Bonus>().Type;
                OnCollisionBonus?.Invoke(bonusType);
                
                Destroy(collision.gameObject);
            }
        }
        
        public void UpdateSkin(Sprite skin)
        {
            _playerSkin.sprite = skin;
        }
    }
}