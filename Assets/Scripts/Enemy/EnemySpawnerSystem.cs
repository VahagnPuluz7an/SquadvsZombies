using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Enemy
{
    [BurstCompile]
    public partial class EnemySpawnerSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<EnemySpawnerConfig>();
        }
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            Enabled = false;
            
            var ecb = new EntityCommandBuffer(Allocator.Persistent);
            
            var config = SystemAPI.GetSingleton<EnemySpawnerConfig>();
            var spawnPointPos = EntityManager.GetComponentData<LocalTransform>(config.SpawnPoint).Position;
            var random = new Random(123);
            
            for (int i = 0; i < config.SpawnCount; i++)
            {
                var newEntity = ecb.Instantiate(config.EnemyPrefab);
                
                ecb.SetComponent(newEntity, new LocalTransform()
                {
                    Position = spawnPointPos + new float3(random.NextFloat(-5f,5f),0, i / 10f),
                    Rotation = quaternion.identity,
                    Scale = 1
                });
            }
            
            ecb.Playback(EntityManager);
        }
    }
}