using Unity.Entities;
using UnityEngine;

namespace Squad
{
    public class BrawlerAuthoring : MonoBehaviour
    {
        [SerializeField] private float shootSpeed;
        
        private class BrawlerBaker : Baker<BrawlerAuthoring>
        {
            public override void Bake(BrawlerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Renderable);
                AddComponent(entity, new BrawlerData()
                {
                    ShootSpeed = authoring.shootSpeed
                });
            }
        }
    }
}