using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class SpawnProjectilesConfigAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int projectilesStartCount;
        [SerializeField] private bool autoExpending;
        
        private class SpawnProjectilesConfigAuthoringBaker : Baker<SpawnProjectilesConfigAuthoring>
        {
            public override void Bake(SpawnProjectilesConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new ProjectilePoolConfig()
                {
                    PrefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                    StartCount = authoring.projectilesStartCount,
                    AutoExpanding = authoring.autoExpending
                });
            }
        }
    }
}