using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Pooling
{
    public partial struct PoolSystem : ISystem
    {
       // private NativeList<ProjectileData> Projectiles { get;  set; }

        public void Shoot()
        {
            Debug.Log("shooott");
        }
    }
    
    public struct PoolData : IComponentData
    {
        public int PoolSize;
    }
}