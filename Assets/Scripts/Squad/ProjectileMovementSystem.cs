using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    [BurstCompile]
    public partial struct ProjectileMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Projectile>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, projectile,entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Projectile>>().WithEntityAccess())
            {
                transform.ValueRW.Position += new float3(0, 0, projectile.ValueRO.Speed) * SystemAPI.Time.DeltaTime;

                if (transform.ValueRO.Position.z > 100)
                {
                    state.EntityManager.SetComponentEnabled<Projectile>(entity, false);
                }
            }
        }
    }
}