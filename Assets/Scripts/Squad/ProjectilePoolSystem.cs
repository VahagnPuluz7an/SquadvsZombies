using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    [BurstCompile]
    public partial class ProjectilePoolSystem : SystemBase
    {
        private bool _startCountSpawned;
        
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<ProjectilePoolConfig>();
        }

        [BurstCompile]
        protected override void OnUpdate()
        {
            var config = SystemAPI.GetSingleton<ProjectilePoolConfig>();
            
            if (!_startCountSpawned)
            {
                SpawnNewProjectiles(config, config.StartCount);
                _startCountSpawned = true;
                return;
            }
            
            bool hasProjectile = false;

            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithDisabled<Projectile>())
            {
                hasProjectile = true;
                break;
            } 
            
            if (hasProjectile || !config.AutoExpanding)
                return;

            SpawnNewProjectiles(config,20);
        }

        [BurstCompile]
        private void SpawnNewProjectiles(ProjectilePoolConfig config ,int count)
        {
            var ecb = new EntityCommandBuffer(Allocator.Persistent);
            for (int i = 0; i < count; i++)
            {
                var newEntity = ecb.Instantiate(config.PrefabEntity);
                
                ecb.SetComponentEnabled<Projectile>(newEntity, false);
                ecb.SetComponent(newEntity, new LocalTransform()
                {
                    Position = new float3(-1000f, -1000f, -1000f),
                    Rotation = quaternion.identity,
                    Scale = 1
                });
            }
            ecb.Playback(EntityManager);
        }
    }
}