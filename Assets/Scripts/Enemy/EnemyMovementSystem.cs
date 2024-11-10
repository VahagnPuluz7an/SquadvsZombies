using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Enemy
{
    public partial struct EnemyMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var movementJob = new EnemyMovementSystemJob()
            {
                DeltaTime = SystemAPI.Time.fixedDeltaTime
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
                transform.Position = Vector3.Lerp(transform.Position, finalPos, DeltaTime * enemyData.MovementSpeed);
            }
        }
    }
}
