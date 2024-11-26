using Unity.Entities;
using Unity.Mathematics;

public struct EnemyData : IComponentData
{
    public float MovementSpeed;
    public bool IsDead;
}

public struct Projectile : IComponentData
{
    public float Speed;
    public float Radius;
}

public struct ShooterBrawlerData : IComponentData
{
    public float ShootInterval;
    public float ShootTimer;
    public Entity ShootPos;
    public Entity Aim;
}

public struct MoneyGiverBrawlerData : IComponentData
{
    public float GiveInterval;
    public float GiveTimer;
}

public enum BrawlerType
{
    Shooter = 0,
    MoneyGiver = 1,
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

public struct ParticleData : IComponentData, IEnableableComponent
{
    public float3 Pos;

    public ParticleData(float3 pos)
    {
        Pos = pos;
    }
}