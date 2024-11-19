using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    [UpdateAfter(typeof(ProjectilePoolSystem))]
    public partial struct ProjectileHideSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Projectile>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform,projectile, entity) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<Projectile>>().WithDisabled<Projectile>().WithEntityAccess())
            {
                transform.ValueRW.Position = new float3(-1000f, -1000f, -1000f);
            }
        }
    }
}