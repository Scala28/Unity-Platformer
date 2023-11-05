using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class E2_AttackState : EnemyAttackState
{
    private Enemy2 enemy;
    public E2_AttackState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        entity.SetVelocityX(0f);
        entity.SetCanTakeDamage(false);
    }

    public override void Exit()
    {
        base.Exit();
        entity.SetCanTakeDamage(true);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void AnimationDealDamage()
    {
        base.AnimationDealDamage();
        if (enemy.CheckPlayerInAttackRange())
        {
            //TODO: implement player Damage
            Debug.Log("Player hit");
        }
        else { Debug.Log("Player not hit"); }
    }
}
