using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : State<Archer>
{
    public ArcherIdleState(Archer archer, StateMachine<Archer> stateMachine) : base(archer, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (creature.CheckPlayer() && (creature.CheckDistance() <= creature.AttackRange))
        {
            StateMachine.ChangeState(creature.attack);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
