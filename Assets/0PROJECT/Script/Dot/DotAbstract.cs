using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public abstract class DotAbstract : MonoBehaviour
{
    public List<Transform> ConnectedDots = new List<Transform>();
    protected SpriteRenderer spriteRenderer;
    protected LineRenderer lineRenderer;
    protected Animator anim;
    protected Vector2 TargetMovePoint;
    protected float CurrentMoveSpeed;

    protected virtual void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, TargetMovePoint, Time.deltaTime * CurrentMoveSpeed);

        if (Vector2.Distance(transform.position, TargetMovePoint) < 0.1f)
            SetNewTarget();
    }

    private void SetNewTarget()
    {
        TargetMovePoint = DotSpawner.GetRandomPointInFOV();
        CurrentMoveSpeed = MoveSpeed();
    }

    float MoveSpeed()
    {
        GameData data = GameManager.Instance.data;
        var speedByDistance = Random.Range(0.1f, 0.3f);
        return speedByDistance /data.MultiplierByScore * data.DotSpeedMultiplier;
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

    public virtual void ResetDotProperties()
    {
        // DOTween.Kill(this.gameObject);
        ConnectedDots.Clear();
        lineRenderer.positionCount = 0;
    }
}
