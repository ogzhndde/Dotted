using System.Collections.Generic;
using UnityEngine;

/// <summary>   
/// Class that contains the basic methods of all dots created.
/// </summary>

public abstract class DotAbstract : MonoBehaviour
{
    public List<Transform> ConnectedDots = new List<Transform>();
    protected SpriteRenderer spriteRenderer;
    protected LineRenderer lineRenderer;
    protected Animator anim;
    protected Vector2 TargetMovePoint;
    protected float CurrentMoveSpeed;

    //Make the dot moves to target
    protected virtual void Move()
    {
        if (GameManager.Instance._gameFail) return;

        transform.position = Vector2.MoveTowards(transform.position, TargetMovePoint, Time.deltaTime * CurrentMoveSpeed);

        if (Vector2.Distance(transform.position, TargetMovePoint) < 0.1f)
            SetNewTarget();
    }

    private void SetNewTarget()
    {
        TargetMovePoint = DotSpawner.GetRandomPointInFOV();
        CurrentMoveSpeed = MoveSpeed();
    }

    //Set movement speed based on score and other factors
    float MoveSpeed()
    {
        GameData data = GameManager.Instance.data;
        var speedByDistance = Random.Range(0.1f, 0.3f);
        return speedByDistance / data.MultiplierByScore * data.DotSpeedMultiplier;
    }

    //Check other dots that the dot connects to
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

    //Draw line between connections with linerenderer
    protected virtual void DrawLine()
    {
        lineRenderer.positionCount = ConnectedDots.Count * 2;
        lineRenderer.SetPositions(ConnectionPoints());
    }

    //Disable dot when creating a closed connection
    protected virtual void ExplodeDot()
    {
        ObjectPool.ReturnObjectToPool(gameObject);
    }

    //Reset dot values when send object to pool
    public virtual void ResetDotProperties()
    {
        ConnectedDots.Clear();
        lineRenderer.positionCount = 0;
    }
}
