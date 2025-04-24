using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Gameplay.Player
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Material _material;
        
        private float _value;
        private readonly int Fill = Shader.PropertyToID("_fill");

        private void Start()
        {
            _value = _material.GetFloat(Fill);
        }

        public void ActiveAnimation(bool isActive)
        {
            if (isActive)
            {
                StartCoroutine(AnimHide());
            }
            else
            {
                StartCoroutine(AnimShow());
            }
        }
        
        IEnumerator AnimHide()
        {
            while (_value > 0.0f)
            {
                _value -= 0.1f;
                _material.SetFloat(Fill, _value);
                yield return null;
            }
        }
        
        IEnumerator AnimShow()
        {
            while (_value < 1)
            {
                _value += 0.1f;
                _material.SetFloat(Fill, _value);
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
                yield return null;
            }
        }
    }
}