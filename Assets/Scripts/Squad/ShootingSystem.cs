using Enemy;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    [BurstCompile]
    [UpdateAfter(typeof(EnemySpawnerSystem))]
    public partial struct ShootingSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BrawlerData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform,brawler) in SystemAPI.Query<RefRW<LocalTransform>,RefRW<BrawlerData>>())
            {
                brawler.ValueRW.ShootTimer -= SystemAPI.Time.DeltaTime;
                
                if (brawler.ValueRO.ShootTimer > 0)
                    continue;

                brawler.ValueRW.ShootTimer = brawler.ValueRO.ShootInterval;

                Aim(ref state, ref brawler.ValueRW, ref transform.ValueRW);
                Shoot(ref state, state.EntityManager.GetComponentData<LocalToWorld>(brawler.ValueRO.ShootPos).Position,
                    state.EntityManager.GetComponentData<LocalToWorld>(brawler.ValueRO.Aim).Position);
            }
        }

        [BurstCompile]
        private void Aim(ref SystemState state, ref BrawlerData brawler, ref LocalTransform brawlerLocal)
        {
            float minDistance = float.MaxValue;
            var closestEnemy = new RefRW<LocalTransform>();
            
            foreach (var enemyTransform in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<ShowEntityTag>().WithAll<EnemyData>())
            {
                float distance = math.distance(enemyTransform.ValueRO.Position, brawlerLocal.Position);
                
                if(distance > minDistance)
                    continue;

                minDistance = distance;
                closestEnemy = enemyTransform;
            }

            var aimTransform = state.EntityManager.GetComponentData<LocalTransform>(brawler.Aim);
            var aimWorldTransform = state.EntityManager.GetComponentData<LocalToWorld>(brawler.Aim);
            aimTransform.Rotation = quaternion.LookRotation(math.normalize(closestEnemy.ValueRO.Position + new float3(0,0.5f,0f) - aimWorldTransform.Position), math.forward());
            state.EntityManager.SetComponentData(brawler.Aim, aimTransform);
        }

        [BurstCompile]
        private void Shoot(ref SystemState state, float3 shootPos, float3 aimPos)
        {
            foreach (var (transform, entity) in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<Projectile>().WithDisabled<ShowEntityTag>().WithEntityAccess())
            {
                state.EntityManager.SetComponentEnabled<ShowEntityTag>(entity, true);
                transform.ValueRW.Position = shootPos;
                transform.ValueRW.Rotation = quaternion.LookRotation(math.normalize(shootPos - aimPos), math.forward());
                break;
            }
        }
    }
}
