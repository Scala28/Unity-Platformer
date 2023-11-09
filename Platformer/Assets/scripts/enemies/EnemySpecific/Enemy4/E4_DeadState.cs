using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_DeadState : EnemyDeadState
{
    private Enemy4 enemy;
    public E4_DeadState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyDeadState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

}
