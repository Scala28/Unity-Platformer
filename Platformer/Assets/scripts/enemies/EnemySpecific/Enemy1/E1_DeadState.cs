using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_DeadState : EnemyDeadState
{
    private Enemy1 enemy;
    public E1_DeadState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyDeadState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
}
