using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryState : GroundedState
{
    public SanctuaryState(Player creature, StateMachine<Player> stateMachine) : base(creature, stateMachine)
    {
    }

    public override void Enter()
    {
        //add level reload here
        GameManager.Instance.SpawnPoint = creature.CurrentSanct;
    }
    public override void InputUpdate()
    {
        base.InputUpdate();
        if(Input.GetKeyDown(KeyCode.Escape)){
            StateMachine.ChangeState(creature.IdleState);
        }
    }
}
