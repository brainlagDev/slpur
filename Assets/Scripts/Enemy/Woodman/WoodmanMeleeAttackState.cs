using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodmanMeleeAttackState : State<Woodman>
{
    
    private float attackRange;
    private bool isAttacking;
    public WoodmanMeleeAttackState(Woodman woodman, StateMachine<Woodman> stateMachine) : base(woodman, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }
}
