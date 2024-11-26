using System;
using Unity.Entities;
using UnityEngine;

namespace Squad.Authoring
{
    public class BrawlerAuthoring : MonoBehaviour
    {
        [SerializeField] private float shootInterval;
        [SerializeField] private GameObject shootPos;
        [SerializeField] private GameObject aimRotate;
        [SerializeField] private BrawlerType brawlerType;
        
        private class BrawlerBaker : Baker<BrawlerAuthoring>
        {
            public override void Bake(BrawlerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                switch (authoring.brawlerType)
                {
                    case BrawlerType.Shooter:
                        AddComponent(entity, new ShooterBrawlerData()
                        {
                            ShootInterval = authoring.shootInterval,
                            ShootPos = GetEntity(authoring.shootPos, TransformUsageFlags.Dynamic),
                            Aim = GetEntity(authoring.aimRotate, TransformUsageFlags.Dynamic),
                        });
                        break;
                    case BrawlerType.MoneyGiver:
                        AddComponent(entity, new MoneyGiverBrawlerData()
                        {
                            GiveInterval = authoring.shootInterval
                        });
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}