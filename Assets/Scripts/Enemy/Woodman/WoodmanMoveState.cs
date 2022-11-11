using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodmanMoveState : State<Woodman>
{
    public WoodmanMoveState(Woodman enemy, StateMachine<Woodman> stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        creature.TeleportOnNewPosition();
    }
    public override void LogicUpdate()
    {
        if(creature.ChangedPos){
            creature.ChangedPos = false;
            creature.Flip();
            StateMachine.ChangeState(creature.rangedAttack);
        }
        base.LogicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
    
}
