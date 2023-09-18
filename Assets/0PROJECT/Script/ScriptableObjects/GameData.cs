using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/GameData", order = 1)]
public class GameData : ScriptableObject
{
    #region CurrentScore
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
    #endregion
    [Space(10)]

    #region MultiplierByScore
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
    #endregion
    [Space(10)]

    #region DotSpeedMultiplier
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
    #endregion
    [Space(10)]

    #region HighScore
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
    #endregion


    [Button]
    public void ResetData()
    {
        CurrentScore = 0;
        MultiplierByScore = 1;
        DotSpeedMultiplier = 1;
    }
}