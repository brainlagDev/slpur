using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <br>State with fixed duration and ability to repeat itself.</br>
/// <br>Check IsFinished  variable for change state (IsFinished is true if state is out of duration)</br>
/// <br>Assign Repeat as true if you want this state to repeat</br>
/// </summary>
public class FixedState : State<Player>
{
    private   float StateDuration = 0;
    private   float StateTime = 0;
    protected bool  IsFinished = false;

    public FixedState(Player creature, StateMachine<Player> stateMachine, float duration) : base(creature, stateMachine)
    {
        StateDuration = duration;
    }

    /// <summary>
    /// base.Enter() call always required
    /// </summary>
    public override void Enter()
    {
        StateTime = 0;
        IsFinished = false;
    }

    public override void Exit()
    {
    }

    public override void InputUpdate()
    {
    }

    /// <summary>
    /// Prefered to check for IsFinished here
    /// </summary>
    public override void LogicUpdate()
    {
    }

    /// <summary>
    /// base.PhysicsUpdate() call always required
    /// </summary>
    public override void PhysicsUpdate()
    {
        if (StateTime >= StateDuration)
            IsFinished = true;
        else
            StateTime += Time.fixedDeltaTime;
    }
}
