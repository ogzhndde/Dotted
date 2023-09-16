using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DotAbstract : MonoBehaviour
{
    [SerializeField] protected Vector2 TargetMovePoint;

    protected virtual void Move()
    {
        transform.position = Vector2.Lerp(transform.position, TargetMovePoint, Time.deltaTime / 2f);
    }

    protected void SetNewTarget()
    {
        if (Vector2.Distance(transform.position, TargetMovePoint) < 0.1f)
        {
            TargetMovePoint = DotController.Instance.GetRandomPointInFOV();
        }
    }

    protected virtual void SendRay()
    {

    }

    protected virtual void ExplodeDot()
    {

    }

    protected virtual void ResetDotProperties()
    {

    }
}
