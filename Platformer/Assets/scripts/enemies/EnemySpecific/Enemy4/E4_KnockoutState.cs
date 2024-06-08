using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_KnockoutState : EnemyKnockoutState
{
    private Enemy4 enemy;
    public E4_KnockoutState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyKnockoutState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.SetVelocityX(0f);
        enemy.SetVelocityY(0f);
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isKnockoutTimeOver)
            stateMachine.ChangeState(enemy.MoveState);
    }
}
