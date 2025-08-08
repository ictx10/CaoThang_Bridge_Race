using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This "PatrolState" script let the Bot partrol on plane
 * Bot will seek the brick on current plane, pick it up and trying to build the bridge and moving up
 */
public class PatrolState : IState<Bot>
{
    int targetBrick;
    public void OnEnter(Bot t)
    {
        targetBrick = Random.Range(2, 7); //set random 2->6 brick as target
        SeekTarget(t);
    }
    public void OnExecute(Bot t) {
        if (t.IsDestination)
        {
            if (t.BrickCount >= targetBrick)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                SeekTarget(t);
            }
        }
    }
    public void OnExit(Bot t) { }
    private void SeekTarget(Bot t)
    {
        /*
            If t.stage is Null, it means Bot change state before bot hit newstagebox
         */
        if (t.stage != null)
        {
            Brick brick = t.stage.SeekBrickPoint(t.colorType);
            if (brick == null) //if Bot found no brick same color as Bot's Color
            {
                t.ChangeState(new AttackState()); //Change state to AttackState
            }
            else //if Bot found brick same color as Bot's Color
            {
                t.SetDestination(brick.transform.position); //Bot will get that brick as target and move to them
            }
        }
        else
        {
            t.SetDestination(t.transform.position);
        }
    }
}
