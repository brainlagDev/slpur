using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : FixedState
{
    public LandState(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine, duration)
    {
    }

    public override void Enter()
    {
        base.Enter();

        creature.Animator.Play("Land");
    }

    public override void Exit()
    {
    }

    public override void InputUpdate()
    {
    }

    public override void LogicUpdate()
    {
        if (IsFinished)
        {
            StateMachine.ChangeState(creature.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
