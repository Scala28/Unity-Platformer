using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected D_EnemyAttackState stateData;
    protected bool
        isDetectingWall,
        isDetectingLedge,
        isAttackTimeOver;

    protected float lastAttackTime;

    public EnemyAttackState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyAttackState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isAttackTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.AttackDuration)
            isAttackTimeOver = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }

    public virtual bool CanAttack()
    {
        return Time.time >= lastAttackTime + stateData.TimeBtwAttack;
    }
}
