using Enemy;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Squad
{
    public partial class BrawlerSpawnSystem : SystemBase
    {
        private int _brawlerIndex;
        private float3 _pos;
        
        protected override void OnCreate()
        {
            BrawlerSpawnShower.Attached += BrawlerSpawnShowerOnAttached;
        }
        
        protected override void OnDestroy()
        {
            BrawlerSpawnShower.Attached -= BrawlerSpawnShowerOnAttached;
        }
        
        protected override void OnUpdate()
        {
            if (_brawlerIndex < 0)
                return;
            
            var ecb = new EntityCommandBuffer(Allocator.Persistent);
            
            foreach (var buffer in SystemAPI.Query<DynamicBuffer<PrefabBrawlerBuffer>>())
            {
                var prefab = buffer[_brawlerIndex].Prefab;
                var instance = ecb.Instantiate(prefab);
                
                ecb.SetComponent(instance, new LocalTransform
                {
                    Position = _pos,
                    Rotation = quaternion.identity,
                    Scale = 1
                });

                _brawlerIndex = -1;
            }
            
            ecb.Playback(EntityManager);
        }
        
        private void BrawlerSpawnShowerOnAttached(int brawlerIndex, float3 pos)
        {
            _brawlerIndex = brawlerIndex;
            _pos = pos;
        }
    }
}