using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentState { get; private set; }
    public PlayerState PrevState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        PrevState = null;
        CurrentState = startingState;
        CurrentState.Enter();
    }
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        PrevState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
    }
}
