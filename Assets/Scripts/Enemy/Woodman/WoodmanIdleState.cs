using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodmanIdleState : State<Woodman>
{
    public WoodmanIdleState(Woodman enemy, StateMachine<Woodman> stateMachine) : base(enemy, stateMachine)
    {
    }
}
