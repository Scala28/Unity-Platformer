using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeadState : EnemyDeadState
{
    private Enemy2 enemy;
    public E2_DeadState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyDeadState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
