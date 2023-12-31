using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>   
/// The class that controls the score earned
/// </summary>
public class EarnScore : MonoBehaviour
{
    private TextMeshPro TMP_EarnAmount => GetComponent<TextMeshPro>();
    public float EarnAmount;

    public void SetValues()
    {
        GetComponent<Animator>().SetTrigger("_isActive");

        TMP_EarnAmount.text = "+" + EarnAmount.ToString("0");

        StartCoroutine(ReturnToPoolAfterTime());
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        float elapsedTime = 0f;

        while (elapsedTime < 1.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ObjectPool.ReturnObjectToPool(gameObject);
    }
}
