using System.Collections;
using System.Collections.Generic;
using DotFactoryStatic;
using UnityEngine;

public class DotSpawner : Singleton<DotSpawner>
{



    void SpawnNewDot()
    {
        DotFactory.SpawnDot(DotType.Standart, GetRandomPointInFOV());
    }


    public Vector2 GetRandomPointInFOV()
    {
        Camera mainCamera = Camera.main;

        float randomX = Random.Range(0f, 1f);
        float randomY = Random.Range(0f, 1f);

        Vector3 randomViewportPoint = new Vector3(randomX, randomY, mainCamera.nearClipPlane);
        Vector3 randomWorldPoint = mainCamera.ViewportToWorldPoint(randomViewportPoint);

        return (Vector2)randomWorldPoint;
    }

    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnStart, OnStart);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
    }

    private void OnStart()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnNewDot();
        }
    }
}
