using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFiniteStateMachine
{
    public EnemyState CurrentState { get; private set; }
    public EnemyState PrevState { get; private set; }

    public void Initialize(EnemyState startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }
    public void ChangeState(EnemyState toState)
    {
        CurrentState.Exit();
        PrevState = CurrentState;
        CurrentState = toState;
        CurrentState.Enter();
    }
}
