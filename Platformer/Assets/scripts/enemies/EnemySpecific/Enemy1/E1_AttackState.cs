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

    public override bool CanAttack()
    {
        return base.CanAttack();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetCanTakeDamage(false);
        enemy.SetVelocityX(stateData.AttackSpeed * entity.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.SetCanTakeDamage(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.IdleState);
        }else if(isAttackTimeOver)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
