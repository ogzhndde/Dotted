using System.Collections;
using System.Collections.Generic;
using ParticleFactoryStatic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>   
/// The class that holds all the dots in the scene, controls their connections and checks the intersection. 
/// In short, the class that controls all dots in the scene.
/// </summary>
public class DotController : Singleton<DotController>
{
    GameManager manager;

    public List<GameObject> AllDotsInScene = new List<GameObject>();
    [SerializeField] private List<GameObject> AllConnectedDots = new List<GameObject>();

    public GameObject CurrentDot;


    [Space(15)]
    public bool _isSwiping = false;

    void Start()
    {
        manager = GameManager.Instance;
    }

    void Update()
    {
        SwipingCheck();
        SetCurrentDot();

        ReorganizeDotConnections();
        CheckAnyIntersection();
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

    //Check the intersection of all interconnected dots
    void CheckAnyIntersection()
    {
        if (manager._gameFail) return;
        if (AllConnectedDots.Count < 3) return;

        int dotCount = AllConnectedDots.Count;

        for (int i = 0; i < dotCount - 1; i++)
        {
            //Get start dot and its connection
            Vector2 p1 = AllConnectedDots[i].transform.position;
            Vector2 p2 = AllConnectedDots[i + 1].transform.position;

            for (int j = 0; j < dotCount - 1; j++)
            {
                if (j != i)
                {
                    //Get other dots and their connections
                    Vector2 p3 = AllConnectedDots[j].transform.position;
                    Vector2 p4 = AllConnectedDots[j + 1].transform.position;

                    //Check interrupt status
                    bool _isIntersect = DotIntersection.AreLinesIntersecting(out Vector3 intersection, p1, p2, p3, p4);

                    if (_isIntersect)
                    {
                        EventManager.Broadcast(GameEvent.OnFinish);
                        EventManager.Broadcast(GameEvent.OnLineIntersection, intersection);
                        ParticleFactory.SpawnParticle(ParticleType.Intersection, intersection);
                        return;
                    }
                }
            }
        }
    }

    //Check the user is swiping or not
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
        if (AllConnectedDots.Count > 0) //If there is any connection, set last dot as current dot
        {
            CurrentDot = AllConnectedDots[^1];
        }
        else //if there is not any connection, set a random point as current dot
        {
            CurrentDot = AllDotsInScene[Random.Range(0, AllDotsInScene.Count)];
            AllConnectedDots.Add(CurrentDot);
        }
    }


    private IEnumerator ClosedConnectionProcess(GameObject selectedDot)
    {
        yield return new WaitForSeconds(0.15f);

        //Control variables of connection
        float connectionLenght = 0;
        int connectedDot = 0;
        int index = AllConnectedDots.IndexOf(selectedDot);

        for (int i = index; i < AllConnectedDots.Count; i++)
        {
            if (i < AllConnectedDots.Count)
                EventManager.Broadcast(GameEvent.OnClosedConnection, AllConnectedDots[i]);

            //Check total lenght of connection and connection dots count for score
            connectedDot++;
            if (i < AllConnectedDots.Count - 1)
                connectionLenght += Vector2.Distance(AllConnectedDots[i].transform.position, AllConnectedDots[i + 1].transform.position);
        }

        //Add score based on the number and length of connections
        EventManager.Broadcast(GameEvent.OnScore, connectionLenght * connectedDot * 2f, selectedDot);

        //Clear lists
        for (int i = AllConnectedDots.Count - 1; i >= index; i--)
        {
            var dot = AllConnectedDots[i];
            AllConnectedDots.Remove(dot);
            AllDotsInScene.Remove(dot);
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

    //Event for connection dot check
    private void OnConnectDot(object value)
    {
        if (!_isSwiping) return;

        GameObject selectedDot = (GameObject)value;

        //Add selected dot 
        if (!AllConnectedDots.Contains(selectedDot))
        {
            AllConnectedDots.Add(selectedDot);
            EventManager.Broadcast(GameEvent.OnPlaySoundPitch, "SoundPop", 1f + (AllConnectedDots.Count * 0.1f));
        }
        else
        {
            if (AllConnectedDots.IndexOf(selectedDot) < AllConnectedDots.Count - 2 && AllConnectedDots.Count >= 3)
            {
                AllConnectedDots.Add(selectedDot);
                StartCoroutine(ClosedConnectionProcess(selectedDot));
            }

        }

    }
}
