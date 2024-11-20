using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Enemy
{
    [BurstCompile]
    public partial struct EnemyMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var movementJob = new EnemyMovementSystemJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            movementJob.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct EnemyMovementSystemJob : IJobEntity
        {
            public float DeltaTime;
            
            [BurstCompile]
            private void Execute(ref LocalTransform transform, in EnemyData enemyData)
            {
                if (transform.Position.z < 0)
                    return;
                transform = transform.Translate(new float3(0, 0, -1 * enemyData.MovementSpeed * DeltaTime));
            }
        }
    }
}
