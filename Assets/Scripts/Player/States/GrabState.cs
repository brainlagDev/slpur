using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabState : State<Player>
{
    private float HorizontalInput = 0;
    private float VerticalInput = 0;
    private float GraviryScale;
    private bool IsGrab = false;
    private float GrabTime;
    private float GrabCounter = 0;

    public GrabState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        creature.Animator.Play("GrabIdle");
        GraviryScale = creature.Rigidbody.gravityScale;
        creature.Rigidbody.gravityScale = 0;
        GrabCounter = 0;
        
        float x = creature.GrabCollision.transform.position.x;
        float y = creature.GrabCollision.transform.position.y;
        if (creature.LeftLedge)
        {
            x += creature.HangLedgeOffset.x;
            y += creature.HangLedgeOffset.y;
        }
        else if (creature.RightLedge)
        {
            x -= creature.HangLedgeOffset.x;
            y -= creature.HangLedgeOffset.y;
        }
        creature.transform.position = new Vector2(x, y);
    }

    public override void Exit()
    {
        creature.Rigidbody.gravityScale = GraviryScale;
    }

    public override void InputUpdate()
    {
        if (IsGrab)
            return;

        HorizontalInput = creature.Input.Horizontal();
        VerticalInput = creature.Input.Vertical();

        if (Input.GetKey(KeyCode.Space))
        {
            StateMachine.ChangeState(creature.JumpState);
        }
    }

    public override void LogicUpdate()
    {
        // Horizontal input
        if (HorizontalInput < 0)
        {
            if (creature.LeftLedge)
            {
                Grab();
            }
            else if (creature.RightLedge)
            {
                StateMachine.ChangeState(creature.FallState);
            }
        }
        else if (HorizontalInput > 0)
        {
            if (creature.RightLedge)
            {
                Grab();
            }
            else if (creature.LeftLedge)
            {
                StateMachine.ChangeState(creature.FallState);
            }
        }
        
        // Vertical input
        if (VerticalInput > 0)
        {
            Grab();
        }
        else if (VerticalInput < 0)
        {
            StateMachine.ChangeState(creature.FallState);
        }
    }

    public override void PhysicsUpdate()
    {
        if (IsGrab)
        {
            if (GrabCounter >= GrabTime)
            {
                creature.transform.position = new Vector2(
                    creature.GrabCollision.transform.position.x + creature.GrabLedgeOffset.x,
                    creature.GrabCollision.transform.position.y + creature.GrabLedgeOffset.y);
                StateMachine.ChangeState(creature.IdleState);
            }
            GrabCounter += Time.fixedDeltaTime;
        }
    }

    private void Grab()
    {
        creature.Animator.Play("Grab");
        IsGrab = true;
    }
}
