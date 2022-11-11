using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : State<Player>
{
    private float FallHorizontalInput = 0;

    public FallState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        creature.Animator.Play("Fall");
        creature.AirTime = 0;
    }

    public override void Exit()
    {
        creature.Rigidbody.velocity = new Vector2(0, creature.Rigidbody.velocity.y);
    }

    public override void InputUpdate()
    {
        FallHorizontalInput = creature.Input.Horizontal();
    }

    public override void LogicUpdate()
    {
        if (creature.IsGrounded)
        {
            if (creature.AirTime >= creature.Settings.LandTime)
                StateMachine.ChangeState(creature.LandState);
            else
                StateMachine.ChangeState(creature.IdleState);
        }

        if (FallHorizontalInput < 0)
        {
            if (!creature.IsFlip)
                creature.Flip();

            if (creature.LeftLedge)
                StateMachine.ChangeState(creature.GrabState);
        }
        else if (FallHorizontalInput > 0)
        {
            if (creature.IsFlip)
                creature.Flip();

            if (creature.RightLedge)
                StateMachine.ChangeState(creature.GrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        creature.Rigidbody.velocity = new Vector2
            (FallHorizontalInput * creature.Settings.MoveSpeed,
             creature.Rigidbody.velocity.y);

        creature.AirTime += Time.fixedDeltaTime;
    }
}
