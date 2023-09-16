using System.Collections.Generic;
using DotFactoryStatic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DotController : Singleton<DotController>
{
    public Transform h2;
    public bool _gameFail = false;
    [SerializeField] public List<GameObject> AllConnectedDots = new List<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnNewDot();
        }
        ReorganizeDotConnections();
        CheckAnyIntersection();
    }

    void SpawnNewDot()
    {
        DotFactory.SpawnDot(DotType.Standart, GetRandomPointInFOV());
    }

    void ReorganizeDotConnections()
    {
        int count = AllConnectedDots.Count;
        for (int i = 0; i < count - 1; i++)
        {
            DotAbstract dotAbstract = AllConnectedDots[i].GetComponent<DotAbstract>();
            dotAbstract.ConnectedDots.Add(AllConnectedDots[i + 1].transform);
        }
    }

    void CheckAnyIntersection()
    {
        if (_gameFail) return;

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
                    bool _isIntersect = AreLinesIntersecting(out Vector3 intersection, p1, p2, p3, p4);
                    Debug.Log(_isIntersect);
                    if (_isIntersect)
                    {
                        _gameFail = true;

                        h2.position = intersection;
                        return;
                    }


                }

            }
        }

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

    bool AreLinesIntersecting(out Vector3 intersection, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
    {
        intersection = Vector3.zero;

        // Offset değeri
        float offset = 0.1f;

        // Offset uygula
        p1 += offset * (p2 - p1).normalized;
        p2 += offset * (p1 - p2).normalized;
        p3 += offset * (p4 - p3).normalized;
        p4 += offset * (p3 - p4).normalized;

        Vector3 dir1 = p2 - p1;
        Vector3 dir2 = p4 - p3;

        float a1 = -dir1.y;
        float b1 = dir1.x;
        float d1 = -(a1 * p1.x + b1 * p1.y);

        float a2 = -dir2.y;
        float b2 = dir2.x;
        float d2 = -(a2 * p3.x + b2 * p3.y);

        float seg1_line2_start = a2 * p1.x + b2 * p1.y + d2;
        float seg1_line2_end = a2 * p2.x + b2 * p2.y + d2;

        float seg2_line1_start = a1 * p3.x + b1 * p3.y + d1;
        float seg2_line1_end = a1 * p4.x + b1 * p4.y + d1;

        if ((seg1_line2_start < 0 && seg1_line2_end > 0) || (seg1_line2_start > 0 && seg1_line2_end < 0))
        {
            if ((seg2_line1_start < 0 && seg2_line1_end > 0) || (seg2_line1_start > 0 && seg2_line1_end < 0))
            {
                float u = seg1_line2_start / (seg1_line2_start - seg1_line2_end);
                intersection = p1 + u * dir1;
                return true;
            }
        }

        return false;
    }


}
