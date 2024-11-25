using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class BrawlerSpawnConfigAuthoring : MonoBehaviour
    {
        [SerializeField] private BrawlersScriptable brawlersScriptable;

        private class BrawlerSpawnConfigAuthoringBaker : Baker<BrawlerSpawnConfigAuthoring>
        {
            public override void Bake(BrawlerSpawnConfigAuthoring authoring)
            {
                var brawlersData = authoring.brawlersScriptable.Brawlers;
                
                var entityArray = new NativeArray<Entity>(brawlersData.Length, Allocator.Persistent);
                
                for (int i = 0; i < brawlersData.Length; i++)
                {
                    entityArray[i] = GetEntity(brawlersData[i].Brawler, TransformUsageFlags.Dynamic);
                }

                var entity = CreateAdditionalEntity(TransformUsageFlags.None);
                AddBuffer<PrefabBrawlerBuffer>(entity).CopyFrom(entityArray.Reinterpret<PrefabBrawlerBuffer>());
                entityArray.Dispose();
            }
        }
    }
}