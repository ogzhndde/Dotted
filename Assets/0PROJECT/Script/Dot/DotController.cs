using System.Collections.Generic;
using DotFactoryStatic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DotController : Singleton<DotController>
{
    public List<GameObject> AllDotsInScene = new List<GameObject>();
    [SerializeField] private List<GameObject> AllConnectedDots = new List<GameObject>();

    public GameObject CurrentDot;


    [Space(15)]
    [SerializeField] private bool _isSwiping = false;

    void Update()
    {
        SwipingCheck();
        SetCurrentDot();

        ReorganizeDotConnections();
        // CheckAnyIntersection();
    }


    void ReorganizeDotConnections()
    {
        int count = AllConnectedDots.Count;
        for (int i = 0; i < count - 1; i++)
        {
            DotAbstract dotAbstract = AllConnectedDots[i].GetComponent<DotAbstract>();
            if (!dotAbstract.ConnectedDots.Contains(AllConnectedDots[i + 1].transform))
                dotAbstract.ConnectedDots.Add(AllConnectedDots[i + 1].transform);
        }
    }

    void CheckAnyIntersection()
    {

        int dotCount = AllConnectedDots.Count;

        for (int i = 0; i < dotCount - 1; i++)
        {
            Vector3 p1 = AllConnectedDots[i].transform.position;
            Vector3 p2 = AllConnectedDots[i + 1].transform.position;

            for (int j = 0; j < dotCount - 1; j++)
            {

                Vector3 p3 = AllConnectedDots[j].transform.position;
                Vector3 p4 = AllConnectedDots[j + 1].transform.position;
                if (p1 != p3)
                {
                    bool _isIntersect = DotIntersection.AreLinesIntersecting(out Vector3 intersection, p1, p2, p3, p4);
                    Debug.Log(_isIntersect);
                    if (_isIntersect)
                    {
                        return;
                    }
                }
            }
        }
    }

    void SwipingCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isSwiping = false;
        }
    }


    void SetCurrentDot()
    {
        if (AllConnectedDots.Count > 0)
        {
            CurrentDot = AllConnectedDots[^1];
        }
        else
        {
            CurrentDot = AllDotsInScene[Random.Range(0, AllDotsInScene.Count)];
            AllConnectedDots.Add(CurrentDot);
        }
    }


    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnConnectDot, OnConnectDot);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnConnectDot, OnConnectDot);
    }

    private void OnConnectDot(object value)
    {
        if (!_isSwiping) return;

        GameObject selectedDot = (GameObject)value;

        if (!AllConnectedDots.Contains(selectedDot))
        {
            AllConnectedDots.Add(selectedDot);
        }
        else
        {
            if (selectedDot != AllConnectedDots[^1])
            {
                int index = AllConnectedDots.IndexOf(selectedDot);
                for (int i = index; i < AllConnectedDots.Count; i++)
                {
                    EventManager.Broadcast(GameEvent.OnClosedConnection, AllConnectedDots[i]);
                }

                for (int i = AllConnectedDots.Count - 1; i >= index; i--)
                {
                    AllConnectedDots.RemoveAt(i);
                }

            }

        }

    }
}
