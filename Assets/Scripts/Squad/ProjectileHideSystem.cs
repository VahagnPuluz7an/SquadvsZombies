using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Rendering;

namespace Squad
{
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
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var (projectile, entity) in SystemAPI.Query<RefRO<Projectile>>().WithEntityAccess())
            {
                if (state.EntityManager.HasComponent<RenderBounds>(entity))
                    continue;
                
                ecb.AddComponent<RenderBounds>(entity);
            }
            
            foreach (var (projectile, entity) in SystemAPI.Query<RefRO<Projectile>>().WithDisabled<Projectile>().WithEntityAccess())
            {
                if (!state.EntityManager.HasComponent<RenderBounds>(entity))
                    continue;
                
                ecb.RemoveComponent<RenderBounds>(entity);
            }
            
            ecb.Playback(state.EntityManager);
        }
    }
}