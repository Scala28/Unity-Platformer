using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_AttackState : EnemyAttackState
{
    private Enemy3 enemy;
    private float timeBtwShots;
    private bool canAttack;
    public E3_AttackState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }


    public override bool CanAttack()
    {
        //return base.CanAttack();
        return true;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.FacePlayer();
        canAttack = enemy.CheckAttack();
        if (canAttack)
        {
            if (Time.time >= timeBtwShots + stateData.TimeBtwAttack)
            {
                enemy.Attack();
                timeBtwShots = Time.time;
            }
        }
        else
            stateMachine.ChangeState(enemy.MoveState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
