using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{
    public IdleState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine) { }

    public override void Enter()
    {
        creature.Animator.Play("Idle");
    }

    public override void Exit()
    {
        
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Horizontal != 0)
        {
            StateMachine.ChangeState(creature.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
    }
}
