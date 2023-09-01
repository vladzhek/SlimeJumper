using System;
using Script.Gameplay.Data;
using UnityEngine;

namespace Script.Gameplay.Mono
{
    public class ParticleMovement : MonoBehaviour
    {
        private void Update()
        {
            var pos = transform.position;
            var movePos = pos.x - Values.PLATFORM_START_SPEED * Time.deltaTime;
            transform.position = new Vector3(movePos, pos.y, pos.z);
        }
    }
}