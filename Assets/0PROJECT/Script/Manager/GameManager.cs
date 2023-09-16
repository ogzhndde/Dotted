using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public Transform r1, r2, b1, b2, h2;

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
        // Vector3 intersection;

        // bool isIntersect = Math3d.AreLinesIntersecting(out intersection, r1.position, r2.position, b1.position, b2.position);
        // Debug.Log(isIntersect);

        // h2.position = isIntersect ? intersection : Vector3.zero;
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
