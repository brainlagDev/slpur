using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<G>
{
    public State<G> CurrentState { get; private set; }
    public void Initialize(State<G> startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State<G> newState)
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }

}
