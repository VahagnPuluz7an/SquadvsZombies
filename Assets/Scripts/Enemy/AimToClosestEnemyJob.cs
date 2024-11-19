using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Enemy
{
    public partial struct AimToClosestEnemyJob : IJobEntity
    {
        public NativeArray<LocalTransform> Enemies;
        public LocalTransform AimTransform;
        
        public void Execute(ref LocalTransform transform, ref BrawlerData data)
        {
            float minDistance = float.MaxValue;
            var closestEnemy = LocalTransform.Identity;
            
            foreach (var enemy in Enemies)
            {
                var distance = math.distance(enemy.Position, transform.Position);
                
                if(distance > minDistance)
                    continue;

                minDistance = distance;
                closestEnemy = enemy;
            }

            AimTransform.Rotation = quaternion.LookRotation(closestEnemy.Position, math.forward());
        }
    }
}