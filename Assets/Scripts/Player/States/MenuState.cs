using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State<Player>
{
    public MenuState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
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
