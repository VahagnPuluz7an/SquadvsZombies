using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class BrawlerAuthoring : MonoBehaviour
    {
        [SerializeField] private float shootInterval;
        [SerializeField] private GameObject shootPos;
        [SerializeField] private GameObject aimRotate;
        
        private class BrawlerBaker : Baker<BrawlerAuthoring>
        {
            public override void Bake(BrawlerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BrawlerData()
                {
                    ShootInterval = authoring.shootInterval,
                    ShootPos = GetEntity(authoring.shootPos, TransformUsageFlags.Dynamic),
                    Aim = GetEntity(authoring.aimRotate, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
}