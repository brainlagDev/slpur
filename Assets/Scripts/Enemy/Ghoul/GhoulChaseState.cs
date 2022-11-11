using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulChaseState : State<Ghoul>
{
    public float AttackRange;
    public GhoulChaseState(Ghoul ghoul, StateMachine<Ghoul> stateMachine) : base(ghoul, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Ghoul enter Chase State");
        AttackRange = creature.AttackRange;
        creature.SetAnimInt(2);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!creature.SeesPlayer)
        {
            Debug.Log("Not in the HotSpot");
            StateMachine.ChangeState(creature.idle);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        if (creature.CheckDistance() <= AttackRange)
        {
            StateMachine.ChangeState(creature.attack);
        }
        if (!creature.CheckIsAttacking())
            creature.Move();
            
    }

    public override void Exit()
    {
        creature.SetAnimInt(0);
        base.Exit();
    }
}
