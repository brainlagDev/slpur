using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : State<Archer>
{
    float nextAttackTimer;
    float AttackRate;
    public ArcherAttackState(Archer archer, StateMachine<Archer> stateMachine) : base(archer, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        AttackRate = creature.AttackRate;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= nextAttackTimer)
        {
            creature.EnemyAttack();
            nextAttackTimer = Time.time + 1f / AttackRate;
        }
        if (creature.SeesPlayer && (creature.CheckDistance() > creature.AttackRange))
        {
            creature.Move();
        }
        else
        {
            StateMachine.ChangeState(creature.idle);
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
    public override void Exit()
    {
        base.Exit();
    }
}
