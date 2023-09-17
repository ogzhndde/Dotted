using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public abstract class DotAbstract : MonoBehaviour
{
    public List<Transform> ConnectedDots = new List<Transform>();
    protected LineRenderer lineRenderer;
    protected Animator anim;
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
        TargetMovePoint = DotSpawner.Instance.GetRandomPointInFOV();
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



    protected virtual void ExplodeDot()
    {
        ObjectPool.ReturnObjectToPool(gameObject);
    }

    protected virtual void ResetDotProperties()
    {
        DOTween.Clear();
        TargetMovePoint = new Vector2(0, 0);
    }
}
