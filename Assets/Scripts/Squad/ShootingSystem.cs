using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Squad
{
    [BurstCompile]
    public partial struct ShootingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BrawlerData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var brawler in SystemAPI.Query<RefRW<BrawlerData>>())
            {
                brawler.ValueRW.ShootTimer -= SystemAPI.Time.DeltaTime;
                
                if (brawler.ValueRO.ShootTimer > 0)
                    continue;

                brawler.ValueRW.ShootTimer = brawler.ValueRO.ShootInterval;
                Shoot(ref state, brawler.ValueRO.ShootPos);
            }
        }

        [BurstCompile]
        private void Shoot(ref SystemState state, float3 shootPos)
        {
            foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithDisabled<Projectile>().WithEntityAccess())
            {
                state.EntityManager.SetComponentEnabled<Projectile>(entity, true);
                transform.ValueRW.Position = shootPos + new float3(0, 0, 1f);
                break;
            }
        }
    }
}
