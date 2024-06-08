using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : EnemyMoveState
{
    private Enemy3 enemy;
    public E3_MoveState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (!isDetectingWall && isDetectingLedge)
            entity.SetVelocityX(stateData.movementSpeed * entity.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.CheckAttack() && !isDetectingWall && isDetectingLedge)
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
        else if (!isDetectingLedge || isDetectingWall)
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
