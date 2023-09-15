using UnityEngine;

/// <summary>
/// SubClass of Particle factory
/// </summary>

public abstract class ParticleProperties
{
    public abstract void SpawnParticle(ParticleType particleType,Vector3 spawnPosition, Transform parent);
    
}
