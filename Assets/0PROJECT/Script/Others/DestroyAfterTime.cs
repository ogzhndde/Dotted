using System.Collections;
using UnityEngine;

/// <summary>
/// The class that sends the object back to the pool with a certain delay
/// </summary>

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _timeToDespawn = 1f;

    private Coroutine _timerCoroutine;

    void OnEnable()
    {
        _timerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _timeToDespawn)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ObjectPool.ReturnObjectToPool(gameObject);
    }
}
