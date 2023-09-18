using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// The class that produces all dots that can be produced.
/// There is a main production class called ParticleFactory, and it provides the production of particles where necessary by pulling the specific data of the particle types.
/// </summary>

namespace DotFactoryStatic
{
    public static class DotFactory
    {
        //It stores all dots types and production classes in a dictionary.
        private static Dictionary<DotType, Func<DotProperties>> dotFactories = new Dictionary<DotType, Func<DotProperties>>
        {
            { DotType.Standart, () => new DotStandart() },
            { DotType.TimeBomb, () => new DotTimeBomb() }
        };

        //The class in which dots are spawned.
        public static DotProperties SpawnDot(DotType dotType, Vector2 spawnPosition)
        {
            if (dotFactories.TryGetValue(dotType, out var factory))
            {
                var dot = factory.Invoke();
                dot.SpawnDot(dotType, spawnPosition);
                return dot;
            }
            else
            {
                return null;
            }
        }
    }

    //All necessary data for dots is drawn from scriptable objects and sent to the factory for production.
    public class DotStandart : DotProperties
    {
        GameManager manager => GameManager.Instance;

        public override void SpawnDot(DotType dotType, Vector2 spawnPosition)
        {
            var spawnedDot = ObjectPool.SpawnObjects(manager.DotData.DotStandart, spawnPosition, Quaternion.identity);
            spawnedDot.GetComponent<DotAbstract>().ResetDotProperties();
            DotController.Instance.AllDotsInScene.Add(spawnedDot);
        }
    }

    public class DotTimeBomb : DotProperties
    {
        GameManager manager => GameManager.Instance;

        public override void SpawnDot(DotType dotType, Vector2 spawnPosition)
        {
            var spawnedDot = ObjectPool.SpawnObjects(manager.DotData.DotTimeBomb, spawnPosition, Quaternion.identity);
            spawnedDot.GetComponent<DotAbstract>().ResetDotProperties();
            DotController.Instance.AllDotsInScene.Add(spawnedDot);
        }
    }
}
