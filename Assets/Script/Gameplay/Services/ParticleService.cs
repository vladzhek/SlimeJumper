using Script.Gameplay.Data;
using UnityEngine;

namespace Script.Gameplay.Services
{
    public class ParticleService
    {
        private StaticDataService _staticDataService;
        
        public ParticleService(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void SpawnParticle(ParticleType ID, Transform transform)
        {
            var particle = _staticDataService.ParticleData.Particles.Find(x => x.ID == ID);
            var effect = GameObject.Instantiate(particle.Particle, transform.position, Quaternion.identity);
            effect.Play();
        }
        
        public void SpawnParticleUI(ParticleType ID, RectTransform transform)
        {
            var particle = _staticDataService.ParticleData.Particles.Find(x => x.ID == ID);
            GameObject.Instantiate(particle.Particle, transform);
        }
    }
}