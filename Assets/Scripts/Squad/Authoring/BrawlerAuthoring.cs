using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class BrawlerAuthoring : MonoBehaviour
    {
        [SerializeField] private float shootInterval;
        [SerializeField] private Transform shootPos;
        
        private class BrawlerBaker : Baker<BrawlerAuthoring>
        {
            public override void Bake(BrawlerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable);
                AddComponent(entity, new BrawlerData()
                {
                    ShootInterval = authoring.shootInterval,
                    ShootPos = authoring.shootPos.position,
                });
            }
        }
    }
}