using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : EnemyMoveState
{
    private Enemy1 enemy;
    private bool isPlayerInAttackRange;
    public E1_MoveState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        if(!isDetectingWall && isDetectingLedge)
            enemy.SetVelocityX(stateData.movementSpeed * entity.FacingDirection);
        isPlayerInAttackRange = enemy.CheckPlayerInAttackRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isPlayerInAttackRange = enemy.CheckPlayerInAttackRange();
        if (isPlayerInAttackRange && !isDetectingWall && isDetectingLedge)
        {
            stateMachine.ChangeState(enemy.AttackState);
        }
        else if (isDetectingWall || !isDetectingLedge)
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
