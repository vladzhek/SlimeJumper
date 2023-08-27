using System;
using Script.Gameplay.Data;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Script.Gameplay.Player
{
    public class PlayerJump : ITickable
    {
        public event Action<float> IsTick;

        private Rigidbody2D _rigidbody;
        private bool _isJumping;
        private bool _onGround;
        private float _jumpForce;
        private float _jumpForceIncrement;
        private bool _isIncreasingForce;
        private float _forceChangeSpeed;

        public void Initialize(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
            _isJumping = false;

            //TODO: Перенести в ScriptableObject
            _jumpForceIncrement = 0.1f; // Инкремент увеличения силы прыжка
            _jumpForce = Values.MIN_JUMP_FORCE_PLAYER;
            _isIncreasingForce = true;
            _forceChangeSpeed = 150.0f;
        }

        public void StartJump()
        {
            _isJumping = true;
        }

        public void EndJump()
        {
            _isJumping = false;
            if (_onGround)
            {
                ForceBody();
            }
            
            _jumpForce = Values.MIN_JUMP_FORCE_PLAYER;
            IsTick?.Invoke(0f);

        }

        public void Tick()
        {
            if (_isJumping)
            {
                if (_isIncreasingForce)
                {
                    _jumpForce = Mathf.Min(_jumpForce + _jumpForceIncrement * _forceChangeSpeed * Time.deltaTime, Values.MAX_JUMP_FORCE_PLAYER);
                    if (_jumpForce >= Values.MAX_JUMP_FORCE_PLAYER)
                    {
                        _isIncreasingForce = false;
                    }
                }
                else
                {
                    _jumpForce = Mathf.Max(Values.MIN_JUMP_FORCE_PLAYER, _jumpForce - _jumpForceIncrement * _forceChangeSpeed * Time.deltaTime);
                    if (_jumpForce <= Values.MIN_JUMP_FORCE_PLAYER)
                    {
                        _isIncreasingForce = true;
                    }
                }

                var mappedValue = MapToNewRange(_jumpForce, Values.MIN_JUMP_FORCE_PLAYER, Values.MAX_JUMP_FORCE_PLAYER);
                IsTick?.Invoke(mappedValue);
            }
        }

        private void ForceBody()
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _onGround = false;
        }
        
        private float MapToNewRange(float value, float originalMin, float originalMax)
        {
            float newMin = 0.1f;
            float newMax = 1.0f;
            
            if (value <= originalMin)
                return newMin;
            if (value >= originalMax)
                return newMax;
            
            float originalRange = originalMax - originalMin;
            float newRange = newMax - newMin;
            return (((value - originalMin) * newRange) / originalRange) + newMin;
        }

        public void OnGround()
        {
            _onGround = true;
        }
    }
}