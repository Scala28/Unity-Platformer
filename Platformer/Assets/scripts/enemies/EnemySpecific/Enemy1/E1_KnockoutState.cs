using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_KnockoutState : EnemyKnockoutState
{
    private Enemy1 enemy;
    public E1_KnockoutState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyKnockoutState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        entity.Flip();
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
