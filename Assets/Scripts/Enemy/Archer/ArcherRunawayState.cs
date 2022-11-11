using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherRunawayState : State<Archer>
{
    public ArcherRunawayState(Archer archer, StateMachine<Archer> stateMachine) : base(archer, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        
    }
    public override void Exit()
    {
        base.Exit();
    }
}
