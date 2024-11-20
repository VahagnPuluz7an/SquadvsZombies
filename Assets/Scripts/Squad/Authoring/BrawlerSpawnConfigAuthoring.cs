using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Zenject;

namespace Squad.Authoring
{
    public class BrawlerSpawnConfigAuthoring : MonoBehaviour
    {
        [SerializeField] private BrawlersScriptable brawlersScriptable;

        private class BrawlerSpawnConfigAuthoringBaker : Baker<BrawlerSpawnConfigAuthoring>
        {
            public override void Bake(BrawlerSpawnConfigAuthoring authoring)
            {
                var prefabs = authoring.brawlersScriptable.Brawlers;
                
                var entityArray = new NativeArray<Entity>(prefabs.Length, Allocator.Persistent);
                
                for (int i = 0; i < prefabs.Length; i++)
                {
                    entityArray[i] = GetEntity(prefabs[i], TransformUsageFlags.Dynamic);
                }

                var entity = CreateAdditionalEntity(TransformUsageFlags.None);
                AddBuffer<PrefabBrawlerBuffer>(entity).CopyFrom(entityArray.Reinterpret<PrefabBrawlerBuffer>());
                entityArray.Dispose();
            }
        }
    }
}