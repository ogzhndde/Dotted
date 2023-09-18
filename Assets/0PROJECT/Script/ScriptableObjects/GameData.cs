using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using Unity.Mathematics;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [Header("Float & Int")]
    [SerializeField] private int currentScore;
    public int CurrentScore
    {
        get { return currentScore; }
        set
        {
            if (value < 0) currentScore = 0;
            else currentScore = value;
        }
    }

    [SerializeField] private float multiplierByScore;
    public float MultiplierByScore
    {
        get
        {
            float normalizedValue = Mathf.InverseLerp(0, 3500, CurrentScore);
            multiplierByScore = Mathf.Lerp(1f, 0.5f, normalizedValue);
            return multiplierByScore;
        }
        set
        {
            multiplierByScore = value;
        }
    }

    [SerializeField] private float dotSpeedMultiplier;
    public float DotSpeedMultiplier
    {
        get { return dotSpeedMultiplier; }
        set
        {
            if (value < 0.5f) dotSpeedMultiplier = 0.5f;
            else if (value > 2.5f) dotSpeedMultiplier = 2.5f;
            else dotSpeedMultiplier = value;
        }
    }

    [SerializeField] private int highScore;
    public int HighScore
    {
        get
        {
            if (currentScore > highScore)
            {
                highScore = currentScore;
                return highScore;
            }
            else
            {
                return highScore;
            }
        }
        set
        {
            highScore = value;
        }
    }


    [Button]
    public void ResetData()
    {
        CurrentScore = 0;
        MultiplierByScore = 1;
        DotSpeedMultiplier = 1;
    }

    void ResetList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = default(T);
        }
    }
}