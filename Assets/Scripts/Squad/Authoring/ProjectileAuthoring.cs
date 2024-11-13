using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private class ProjectileBaker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Projectile()
                {
                    Speed = authoring.speed
                });
            }
        }
    }
}