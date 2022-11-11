using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public MoveState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine) { }

    public override void Enter()
    {
        creature.Animator.Play("Move");
    }

    public override void Exit()
    {
        creature.Rigidbody.velocity = new Vector2(0, creature.Rigidbody.velocity.y);
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Horizontal == 0)
        {
            StateMachine.ChangeState(creature.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        creature.Rigidbody.velocity = new Vector2
            (Horizontal * creature.Settings.MoveSpeed,
             creature.Rigidbody.velocity.y);
    }
}
