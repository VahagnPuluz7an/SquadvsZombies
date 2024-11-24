using System;
using Unity.Mathematics;
using UnityEngine;

namespace Particles
{
    public class ParticlesPool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem hitFX;
        [SerializeField] private int hitFXCount;

        private ParticleSystem[] _hitFxs;
        
        private void Awake()
        {
            ParticlePlaySystem.ParticleEnabled += PlayHitFx;
            
            _hitFxs = new ParticleSystem[hitFXCount];

            for (int i = 0; i < hitFXCount; i++)
            {
                var newFx = Instantiate(hitFX, transform);
                _hitFxs[i] = newFx;
            }
        }

        private void OnDestroy()
        {
            ParticlePlaySystem.ParticleEnabled -= PlayHitFx;
        }

        private void PlayHitFx(float3 pos)
        {
            foreach (var fx in _hitFxs)
            {
                if (fx.isPlaying)
                    continue;

                fx.transform.position = pos;
                fx.Play();
                return;
            }
        }
    }
}