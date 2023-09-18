using System.Collections;
using System.Collections.Generic;
using DotFactoryStatic;
using UnityEngine;

/// <summary>   
/// Class that controls dot spawning.
/// </summary>
public class DotSpawner : Singleton<DotSpawner>
{
    [SerializeField] GameData data;

    [SerializeField] private float _defaultTimeToSpawn = 2f;

    public static void SpawnDot()
    {
        if(GameManager.Instance._gameFail) return;
        
        DotType dotType = Random.value > 0.85f ? DotType.TimeBomb : DotType.Standart;
        DotFactory.SpawnDot(dotType, GetRandomPointInFOV());
    }

    private IEnumerator SpawnDotByTime()
    {
        float elapsedTime = 0f;
        float spawnTimer = _defaultTimeToSpawn * data.MultiplierByScore / SpawnMultiplierSpeedByDotCount();

        while (elapsedTime < spawnTimer)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SpawnDot();
        StartCoroutine(SpawnDotByTime());
    }

    float SpawnMultiplierSpeedByDotCount()
    {
        float normalizedMultiplier = Mathf.InverseLerp(0, 15, DotController.Instance.AllDotsInScene.Count);
        float value = Mathf.Lerp(5f, 1f, normalizedMultiplier);
        return value;
    }


    public static Vector2 GetRandomPointInFOV()
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
        for (int i = 0; i < 15; i++)
        {
            SpawnDot();
        }

        StartCoroutine(SpawnDotByTime());
    }
}
