using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    
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
        
    }

    void Update()
    {

    }



    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        // EventManager.AddHandler(GameEvent.OnStart, OnStart);
    }

    private void OnDisable()
    {
        // EventManager.RemoveHandler(GameEvent.OnStart, OnStart);
    }

}
