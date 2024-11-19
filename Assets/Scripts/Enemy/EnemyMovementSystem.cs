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
            
            private void Execute(ref LocalTransform transform, in EnemyData enemyData)
            {
                var finalPos = transform.Position;
                finalPos.z = 0;
                transform.Position = math.lerp(transform.Position, finalPos, DeltaTime * enemyData.MovementSpeed);
            }
        }
    }
}
