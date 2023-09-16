using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class DotAbstract : MonoBehaviour
{
    public List<Transform> ConnectedDots = new List<Transform>();
    protected LineRenderer lineRenderer;
    protected Vector2 TargetMovePoint;

    protected virtual void Move()
    {
        transform.DOMove(TargetMovePoint, MoveSpeed()).SetEase(Ease.Linear).OnComplete(() =>
        {
            SetNewTarget();
            Move();
        });
    }

    private void SetNewTarget()
    {
        TargetMovePoint = DotController.Instance.GetRandomPointInFOV();
    }

    float MoveSpeed()
    {
        float speedByDistance = Vector2.Distance(transform.position, TargetMovePoint) * Random.Range(2f, 4f);
        return speedByDistance;
    }

    Vector3[] ConnectionPoints()
    {
        List<Vector3> connectionArray = new List<Vector3>();
        for (int i = 0; i < ConnectedDots.Count; i++)
        {
            connectionArray.Add(transform.position);
            connectionArray.Add(ConnectedDots[i].position);
        }
        return connectionArray.ToArray();
    }

    protected virtual void DrawLine()
    {
        lineRenderer.positionCount = ConnectedDots.Count * 2;
        lineRenderer.SetPositions(ConnectionPoints());
    }

    protected virtual void SendLinecast()
    {
        Vector3[] connections = ConnectionPoints();

        for (int i = 0; i < connections.Length; i++)
        {
            if (i % 2 == 1)
                Linecast(connections[i - 1], connections[i]);
        }
    }
    private void Linecast(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("sended linecast");
        // Create a layer mask that excludes the "Dot" layer
        int layerMask = ~LayerMask.GetMask("Dot");

        RaycastHit hit;
        if (Physics.Linecast(startPos, endPos, out hit, layerMask))
        {
            Debug.Log("Linecast intersected with object: " + hit.collider.gameObject.name);
            Debug.Log("Intersection point: " + hit.point);
        }
    }

    protected virtual void ExplodeDot()
    {

    }

    protected virtual void ResetDotProperties()
    {
        DOTween.Clear();
        TargetMovePoint = new Vector2(0, 0);
    }
}
