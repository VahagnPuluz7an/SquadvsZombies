using Unity.Entities;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int spawnCount;
        [SerializeField] private GameObject spawnPoint;
        
        private class EnemySpawnerAuthoringBaker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.WorldSpace);
                AddComponent(entity, new EnemySpawnerConfig()
                {
                    EnemyPrefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic),
                    SpawnCount = authoring.spawnCount,
                    SpawnPoint = GetEntity(authoring.spawnPoint, TransformUsageFlags.Dynamic),
                });
            }
        }
    }
}