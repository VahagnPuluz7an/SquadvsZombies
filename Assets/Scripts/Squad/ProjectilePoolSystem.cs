using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    [BurstCompile]
    public partial class ProjectilePoolSystem : SystemBase
    {
        [BurstCompile]
        protected override void OnCreate()
        {
            RequireForUpdate<ProjectilePoolConfig>();
        }
    
        [BurstCompile]
        protected override void OnUpdate()
        {
            Enabled = false;
            var config = SystemAPI.GetSingleton<ProjectilePoolConfig>();
            
            for (int i = 0; i < config.StartCount; i++)
            {
                var newEntity = EntityManager.Instantiate(config.PrefabEntity);

                EntityManager.SetComponentData(newEntity, new LocalTransform()
                {
                    Position = float3.zero,
                    Rotation = quaternion.identity,
                    Scale = 1
                });
                EntityManager.SetComponentEnabled<Projectile>(newEntity, false);
            }
        }
    }
}