using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_DeadState : EnemyDeadState
{
    private Enemy3 enemy;
    public E3_DeadState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyDeadState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
