using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotStandart : DotAbstract
{

    private void Update()
    {
        DrawLine();
        // SendRaycast();
    }

    private void OnMouseEnter()
    {
        EventManager.Broadcast(GameEvent.OnConnectDot, gameObject);
    }



    //########################################    EVENTS    ###################################################################
    private void OnEnable()
    {
        //Appear Process
        lineRenderer = GetComponent<LineRenderer>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("DotAppear");

        TargetMovePoint = transform.position;
        Move();

        //Events
        EventManager.AddHandler(GameEvent.OnClosedConnection, OnClosedConnection);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnClosedConnection, OnClosedConnection);
    }

    private void OnClosedConnection(object dot)
    {
        GameObject targetDot = (GameObject)dot;

        if (ConnectedDots.Contains(targetDot.transform))
            ConnectedDots.Remove(targetDot.transform);

        if (targetDot != gameObject) return;
        ExplodeDot();
    }

}
