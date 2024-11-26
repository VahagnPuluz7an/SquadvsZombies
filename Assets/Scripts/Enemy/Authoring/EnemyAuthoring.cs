using Unity.Entities;
using UnityEngine;

namespace Enemy
{
    public class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float maxHp;
        
        private class EnemyBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyData()
                {
                    MovementSpeed = authoring.movementSpeed,
                });
                AddComponent(entity, new ShowEntityTag());
                AddComponent(entity, new ParticleData());
            }
        }
    }
}
