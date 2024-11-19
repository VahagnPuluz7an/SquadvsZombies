using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    [UpdateAfter(typeof(ProjectilePoolSystem))]
    public partial struct HideSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ShowEntityTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var transform in SystemAPI.Query<RefRW<LocalTransform>>().WithDisabled<ShowEntityTag>())
            {
                transform.ValueRW.Position = new float3(-1000f, -1000f, -1000f);
            }
        }
    }
}