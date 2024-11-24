using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float physicsRadius;
        
        private class ProjectileBaker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Projectile()
                {
                    Speed = authoring.speed,
                    Radius = authoring.physicsRadius,
                });
                AddComponent(entity, new ShowEntityTag());
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(transform.position,physicsRadius);
        }
    }
}