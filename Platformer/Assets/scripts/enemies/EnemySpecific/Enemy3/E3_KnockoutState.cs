using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_KnockoutState : EnemyKnockoutState
{
    private Enemy3 enemy;

    public E3_KnockoutState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyKnockoutState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isKnockoutTimeOver)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }
}
