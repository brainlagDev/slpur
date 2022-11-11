using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : FixedState
{
    private StunData StunData;

    public StunnedState(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine, duration)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (StunData.StunTime != 0)
        {
            creature.Animator.Play("Spawn");
        }
        if (StunData.DiscardForce != 0)
        {
            creature.Rigidbody.AddForce(
                new Vector2(-creature.transform.localScale.x * StunData.DiscardForce, 0), 
                ForceMode2D.Impulse);
        }
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
            StateMachine.ChangeState(creature.IdleState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetStunData(StunData stunData)
    {
        StunData = stunData;
    }

}
