using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckState : GroundedState
{
    public DuckState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        creature.Animator.Play("LowIdle");
        creature.DuckCollider.enabled = false;
    }

    public override void Exit()
    {
        creature.DuckCollider.enabled = true;
    }

    public override void InputUpdate()
    {
        Horizontal = creature.Input.Horizontal();

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StateMachine.ChangeState(creature.IdleState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Horizontal != 0)
        {
            creature.Animator.Play("LowMove");
        }
        else
        {
            creature.Animator.Play ("LowIdle");
        }
    }

    public override void PhysicsUpdate()
    {
        creature.Rigidbody.velocity = new
            Vector2(Horizontal * creature.Settings.DuckSpeed,
            creature.Rigidbody.velocity.y);
    }
}
