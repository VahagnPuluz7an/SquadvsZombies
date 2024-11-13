using Unity.Entities;
using Unity.Mathematics;

public struct EnemyData : IComponentData
{ 
    public float MovementSpeed;
}

public struct Projectile : IComponentData, IEnableableComponent
{
    public float Speed;
}

public struct BrawlerData : IComponentData
{
    public float ShootInterval;
    public float ShootTimer;
    public float3 ShootPos;
}

public struct ProjectilePoolConfig : IComponentData
{
    public Entity PrefabEntity;
    public int StartCount;
    public bool AutoExpanding;
}