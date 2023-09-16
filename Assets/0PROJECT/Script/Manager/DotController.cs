using DotFactoryStatic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DotController : Singleton<DotController>
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNewDot();
        }
    }

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
}
