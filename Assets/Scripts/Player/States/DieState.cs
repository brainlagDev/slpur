using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : FixedState
{
    public DieState(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine, duration)
    {
    }

    public override void Enter()
    {
        base.Enter();

        creature.Animator.Play("Die");
        GameManager.Instance.ReloadLevel();
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
            //
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
