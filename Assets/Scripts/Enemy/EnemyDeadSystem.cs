using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Enemy
{
    [BurstCompile]
    public partial class EnemyDeadSystem : SystemBase
    {
        public static event Action<float3> EnemyDead;
        
        [BurstCompile]
        protected override void OnUpdate()
        {
            foreach (var (enemy,entity) in SystemAPI.Query<RefRW<EnemyData>>().WithEntityAccess())
            {
                if (!enemy.ValueRO.IsDead)
                    continue;
                
                EntityManager.SetComponentEnabled<ShowEntityTag>(entity, false);
                EntityManager.SetComponentEnabled<ParticleData>(entity, true);
                EnemyDead?.Invoke(EntityManager.GetComponentData<LocalTransform>(entity).Position);
                enemy.ValueRW.IsDead = false;
            }
        }
    }
}