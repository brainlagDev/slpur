using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAttackState : State<Ghoul>
{
    public float AttackRange;
    private bool Attacking;

    public GhoulAttackState(Ghoul ghoul, StateMachine<Ghoul> stateMachine) : base(ghoul, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Ghoul enter Attack State");
        AttackRange = creature.AttackRange;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Attacking = creature.CheckIsAttacking();
        if(creature.CheckDistance() > AttackRange && !Attacking)
        {
            StateMachine.ChangeState(creature.chase);
        }
    }

    public override void PhysicsUpdate()
    {
        creature.EnemyAttack();
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
