using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : EnemyIdleState
{
    private Enemy2 enemy;
    public E2_IdleState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
