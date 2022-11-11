using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimation : FixedState
{
    public StateAnimation(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine, duration)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void InputUpdate()
    {
        base.InputUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
