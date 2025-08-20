using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;


    //Check target's position
    private Vector3 destination;
    //public bool IsDestination => Vector3.Distance(destination, transform.position) < 0.1f;
    public bool IsDestination => Vector3.Distance(destination, Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;

    public void SetDestination(Vector3 pos)
    {
        agent.enabled = true;
        destination = pos;
        destination.y = 0;
        agent.SetDestination(pos);
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim("idle");
    }



    //Controlling Bot's stage
    IState<Bot> currentState;

    /*protected override void Start()
    {
        base.Start();
        ChangeState(new PatrolState());
    }*/
    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) && currentState != null)
        {
            currentState.OnExecute(this);
            //TODO: Check stair
            CanMove(transform.position);
        }
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void MoveStop()
    {
        agent.enabled = false;
    }
}
