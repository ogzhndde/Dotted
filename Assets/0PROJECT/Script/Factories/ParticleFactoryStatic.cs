using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
            { ParticleType.Blop, () => new ParticleBlop() },
            { ParticleType.EarnScore, () => new ParticleScore() },
        };

        //The class in which particles are spawned.
        public static ParticleProperties SpawnParticle(ParticleType particleType, Vector3 spawnPosition, float scoreValue = 0f)
        {
            if (particleFactories.TryGetValue(particleType, out var factory))
            {
                var particle = factory.Invoke();
                particle.SpawnParticle(particleType, spawnPosition, scoreValue);
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

        public override void SpawnParticle(ParticleType particleType, Vector3 spawnPosition, float scoreValue)
        {
            manager = GameManager.Instance;

            // var spawnedParticle = ObjectPool.SpawnObjects(manager.SO.ParticleData.HappyParticle, spawnPosition, Quaternion.identity, PoolType.ParticleSystem);
        }
    }

    public class ParticleScore : ParticleProperties
    {
        GameManager manager;

        public override void SpawnParticle(ParticleType particleType, Vector3 spawnPosition, float scoreValue)
        {
            manager = GameManager.Instance;

            var spawnedParticle = ObjectPool.SpawnObjects(manager.ParticleData.EarnScoreParticle, spawnPosition, Quaternion.identity, PoolType.ParticleSystem);

            spawnedParticle.GetComponent<EarnScore>().EarnAmount = scoreValue;
            spawnedParticle.GetComponent<EarnScore>().SetValues();
        }
    }
}
