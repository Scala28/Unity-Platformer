using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_AttackState : EnemyAttackState
{
    protected Enemy1 enemy;
    public E1_AttackState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(stateData.AttackSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAttackTimeOver)
        {
            enemy.IdleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.IdleState);
        }else if(isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
