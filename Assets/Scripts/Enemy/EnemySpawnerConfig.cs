using Unity.Entities;

namespace Enemy
{
    public struct EnemySpawnerConfig : IComponentData
    {
        public Entity EnemyPrefab;
        public int SpawnCount;
        public float Spread;
        public Entity SpawnPoint;
    }
}