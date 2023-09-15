using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// The class that produces all particles that can be produced.
/// There is a main production class called ParticleFactory, and it provides the production of particles where necessary by pulling the specific data of the particle types.
/// </summary>

namespace ParticleFactoryStatic
{
    public static class ParticleFactory
    {
        //It stores all particle types and production classes in a dictionary.
        private static Dictionary<ParticleType, Func<ParticleProperties>> particleFactories = new Dictionary<ParticleType, Func<ParticleProperties>>
        {
            { ParticleType.Blop, () => new ParticleBlop() }
        };

        //The class in which particles are spawned.
        public static ParticleProperties SpawnParticle(ParticleType particleType, Vector3 spawnPosition, Transform parent)
        {
            if (particleFactories.TryGetValue(particleType, out var factory))
            {
                var particle = factory.Invoke();
                particle.SpawnParticle(particleType, spawnPosition, parent);
                return particle;
            }
            else
            {
                return null;
            }
        }
    }

    //All necessary data for particles is drawn from scriptable objects and sent to the factory for production.
    public class ParticleBlop : ParticleProperties
    {
        GameManager manager;

        public override void SpawnParticle(ParticleType particleType, Vector3 spawnPosition, Transform parent)
        {
            manager = GameManager.Instance;

            // var spawnedParticle = ObjectPool.SpawnObjects(manager.SO.ParticleData.HappyParticle, spawnPosition, Quaternion.identity, PoolType.ParticleSystem);
        }
    }
}
