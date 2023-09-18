using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public bool _gameFail = false;

    [Header("ScriptableObjects")]
    public GameData data;
    public DotSO DotData;
    public ParticleSO ParticleData;

    void Awake()
    {
#if !UNITY_EDITOR
        SaveManager.LoadData(data);
#endif
    }

    void Start()
    {
        EventManager.Broadcast(GameEvent.OnStart);
    }



    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
         EventManager.AddHandler(GameEvent.OnStart, OnStart);
         EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
    }

    private void OnDisable()
    {
         EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
         EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
    }

    private void OnStart()
    {
        data.ResetData();
    }


    private void OnFinish()
    {
        _gameFail = true;
    }
}
