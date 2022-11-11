using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : State<Player>
{
    private float HorizontalInput;
    private float VerticalInput;
    private float GravityScale;

    public ClimbState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        creature.Animator.Play("Climb");
        GravityScale = creature.Rigidbody.gravityScale;
        creature.Rigidbody.gravityScale = 0;
        creature.Rigidbody.velocity = Vector2.zero;

        HorizontalInput = creature.Input.Horizontal();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(creature.transform.position, creature.Settings.ClimbRadius);
        foreach (var item in colliders)
        {
            if (item.CompareTag("Ladder"))
            {
                creature.transform.position = new Vector3(
                    item.transform.position.x,
                    creature.transform.position.y,
                    creature.transform.position.z);
                return;
            }
        }
    }

    public override void Exit()
    {
        creature.Rigidbody.gravityScale = GravityScale;
        creature.Animator.speed = 1;
    }

    public override void InputUpdate()
    {
        HorizontalInput = creature.Input.Horizontal();
        VerticalInput = creature.Input.Vertical();

        if (Input.GetKey(KeyCode.Space))
        {
            StateMachine.ChangeState(creature.JumpState);
        }
    }
    
    public override void LogicUpdate()
    {
        if (!creature.CanClimb)
            StateMachine.ChangeState(creature.IdleState);

        if (VerticalInput == 0)
            creature.Animator.speed = 0;
        else
            creature.Animator.speed = 1;

        if (HorizontalInput != 0 || !creature.CanClimb)
        {
            StateMachine.ChangeState(creature.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        creature.Rigidbody.velocity = new
            Vector2(0, VerticalInput * creature.Settings.ClimbSpeed);
    }
}
