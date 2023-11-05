using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockoutState : EnemyState
{
    protected D_EnemyKnockoutState stateData;
    protected bool isKnockoutTimeOver;
    public EnemyKnockoutState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyKnockoutState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityX(stateData.KnockoutSpeed.x * entity.GetDamageDirection().x);
        entity.SetVelocityY(stateData.KnockoutSpeed.y * entity.GetDamageDirection().y);
        entity.CallDamageFlash();
        isKnockoutTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.KnockoutDuration)
        {
            isKnockoutTimeOver = true;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
