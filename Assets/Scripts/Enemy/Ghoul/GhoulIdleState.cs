using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulIdleState : State<Ghoul>
{
    public GhoulIdleState(Ghoul ghoul, StateMachine<Ghoul> stateMachine) : base(ghoul, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Ghoul enter Idle State");
        creature.SetAnimInt(0);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (creature.SeesPlayer)
        {
            StateMachine.ChangeState(creature.chase);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
