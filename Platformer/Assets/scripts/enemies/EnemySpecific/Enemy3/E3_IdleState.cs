using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_IdleState : EnemyIdleState
{
    private Enemy3 enemy;
    public E3_IdleState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void AnimationDealDamage()
    {
        base.AnimationDealDamage();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
