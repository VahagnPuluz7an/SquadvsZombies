using Pooling;
using Unity.Burst;
using Unity.Entities;

namespace Squad
{
    public partial struct ShootingSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            //var poolingSystem = SystemAPI.GetSingleton<PoolData>();

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var job = new ShootingSystemJob()
            {
          //      PoolSystem = poolingSystem
            };
           // job.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct ShootingSystemJob : IJobEntity
        {
            public PoolSystem PoolSystem;
            
            public void Execute()
            {
                PoolSystem.Shoot();
            }
        }
    }
}
