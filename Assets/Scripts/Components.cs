using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct EnemyData : IComponentData
{
    public float MovementSpeed;
}

public struct Projectile : IComponentData
{
    public float Speed;
    public float Radius;
}

public struct BrawlerData : IComponentData
{
    public float ShootInterval;
    public float ShootTimer;
    public Entity ShootPos;
    public Entity Aim;
}

public struct ProjectilePoolConfig : IComponentData
{
    public Entity PrefabEntity;
    public int StartCount;
    public bool AutoExpanding;
}

public struct PrefabBrawlerBuffer : IBufferElementData
{
    public Entity Prefab;
}