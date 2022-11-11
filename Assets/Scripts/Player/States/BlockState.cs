using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : GroundedState
{
    public BlockState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        creature.Animator.Play("Block");
        creature.IsBlock = true;
        creature.StartCoroutine(creature.Parrying());
    }

    public override void Exit()
    {
        creature.IsBlock = false;
    }

    public override void InputUpdate()
    {
        Horizontal = creature.Input.Horizontal();
        if (Input.GetMouseButtonUp(1))
        {
            StateMachine.ChangeState(creature.IdleState);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        creature.Rigidbody.velocity = new Vector2
            (Horizontal * creature.Settings.MoveSpeed / 2,
             creature.Rigidbody.velocity.y);
    }
}
