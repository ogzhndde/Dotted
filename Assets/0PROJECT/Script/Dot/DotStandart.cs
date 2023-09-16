using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotStandart : DotAbstract
{

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        TargetMovePoint = transform.position;

        Move();
    }

    private void Update()
    {
        DrawLine();
        // SendRaycast();
    }

}
