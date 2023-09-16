using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotStandart : DotAbstract
{

    void Start()
    {
        TargetMovePoint = transform.position;

        InvokeRepeating(nameof(SetNewTarget), 0f, 0.2f);
    }

    private void Update()
    {
        Move();
    }
}
