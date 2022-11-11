using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : FixedState
{
    float GravityScale;
    public DashState(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine, duration)
    {
    }

    public override void Enter()
    {
        base.Enter();

        creature.Animator.Play("Dash");

        int dir = 1;
        if (creature.IsFlip)
            dir = -1;

        creature.Rigidbody.velocity = Vector2.zero;
        GravityScale = creature.Rigidbody.gravityScale;
        creature.Rigidbody.gravityScale = 0;

        creature.Rigidbody.AddForce(
            new Vector2(dir * creature.Settings.DashDistance, 0), 
            ForceMode2D.Impulse);
    }

    public override void Exit()
    {
        creature.Rigidbody.gravityScale = GravityScale;
        creature.Rigidbody.velocity = new Vector2(0, creature.Rigidbody.velocity.y);
    }

    public override void InputUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (IsFinished)
            StateMachine.ChangeState(creature.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
