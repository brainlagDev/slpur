using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State<Player>
{
    private float JumpHorizontalInput = 0;

    public JumpState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine) { }

    public override void Enter()
    {
        creature.Animator.Play("Jump");
        creature.Rigidbody.velocity = Vector2.up * creature.Settings.JumpForce;
        //creature.Rigidbody.AddForce(Vector2.up * creature.Settings.JumpForce);
    }

    public override void Exit()
    {
        creature.Rigidbody.velocity = new Vector2(0, creature.Rigidbody.velocity.y);
    }

    public override void InputUpdate()
    {
        JumpHorizontalInput = creature.Input.Horizontal();
    }

    public override void LogicUpdate()
    {
        if (creature.Rigidbody.velocity.y <= 0 && 
            !creature.IsGrounded)
        {
            StateMachine.ChangeState(creature.FallState);
        }

        if (creature.IsGrounded)
        {
            StateMachine.ChangeState(creature.IdleState);
        }

        if (JumpHorizontalInput < 0)
        {
            if (!creature.IsFlip)
                creature.Flip();

            if (creature.LeftLedge)
                StateMachine.ChangeState(creature.GrabState);
        }
        else if (JumpHorizontalInput > 0)
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
            (JumpHorizontalInput * creature.Settings.MoveSpeed,
             creature.Rigidbody.velocity.y);
    }
}
