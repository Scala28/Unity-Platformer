using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected D_EnemyIdleState stateData;
    protected bool
        flipAfterIdle,
        isIdleTimeOver,
        isDetectingWall,
        isDetectingLedge;
    protected float idleTime;
    public EnemyIdleState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyIdleState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();
        if (flipAfterIdle)
        {
            flipAfterIdle = false;
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }
    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime); 
    }
}
