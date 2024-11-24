using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Particles
{
    [BurstCompile]
    public partial class ParticlePlaySystem : SystemBase
    {
        public static event Action<float3> ParticleEnabled;
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            foreach (var (particle, entity) in SystemAPI.Query<RefRW<ParticleData>>().WithEntityAccess())
            {
                ParticleEnabled?.Invoke(particle.ValueRO.Pos);
                EntityManager.SetComponentEnabled<ParticleData>(entity, false);
            }
        }
    }
}