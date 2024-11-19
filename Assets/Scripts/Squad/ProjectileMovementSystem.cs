using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;

namespace Squad
{
    [BurstCompile]
    public partial struct ProjectileMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<Projectile>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
            var collisionWorld = physicsWorldSingleton.CollisionWorld;
            var hitList = new NativeList<DistanceHit>(Allocator.Temp);

            var collisionFilter = new CollisionFilter
            {
                BelongsTo = ~0u,
                CollidesWith = 1u << GameAssets.UNITS_LAYER,
                GroupIndex = 0,
            };
                
            foreach (var (transform, projectile,entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Projectile>>().WithEntityAccess())
            {
                hitList.Clear();
                transform.ValueRW.Position += transform.ValueRW.Forward() * SystemAPI.Time.DeltaTime * projectile.ValueRO.Speed;
                collisionWorld.OverlapSphere(transform.ValueRO.Position, projectile.ValueRO.Radius, ref hitList, collisionFilter);
                
                if (transform.ValueRO.Position.z > 100 || !hitList.IsEmpty)
                {
                    state.EntityManager.SetComponentEnabled<ShowEntityTag>(entity, false);

                    foreach (var hit in hitList)
                    {
                        state.EntityManager.SetComponentEnabled<ShowEntityTag>(hit.Entity, false);
                    }
                }
            }
        }
    }
}