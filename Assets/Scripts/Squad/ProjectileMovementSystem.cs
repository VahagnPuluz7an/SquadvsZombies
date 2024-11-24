using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
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
            var hitList = new NativeList<DistanceHit>(Allocator.Persistent);
            
            var collisionFilter = new CollisionFilter
            {
                BelongsTo = ~0u,
                CollidesWith = 1u << GameAssets.UNITS_LAYER,
                GroupIndex = 0,
            };
            
            foreach (var (transform, projectile,entity) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<Projectile>>().WithEntityAccess())
            {
                hitList.Clear();
                var newVector = transform.ValueRW.Forward() * SystemAPI.Time.DeltaTime * projectile.ValueRO.Speed;
                newVector.y = 0;
                transform.ValueRW.Position += newVector;
                collisionWorld.OverlapSphere(transform.ValueRO.Position, projectile.ValueRO.Radius, ref hitList, collisionFilter);

                if (math.length(transform.ValueRO.Position) > 100)
                {
                    state.EntityManager.SetComponentEnabled<ShowEntityTag>(entity, false);
                    continue;
                }

                if (hitList.IsEmpty)
                    continue;
                
                state.EntityManager.SetComponentEnabled<ShowEntityTag>(entity, false);
      
                
                foreach (var hit in hitList)
                {
                    state.EntityManager.SetComponentEnabled<ShowEntityTag>(hit.Entity, false);
                    
                    state.EntityManager.SetComponentData(hit.Entity, new ParticleData(pos: hit.Position));
                    state.EntityManager.SetComponentEnabled<ParticleData>(hit.Entity, true);
                }
            }
        }
    }
}