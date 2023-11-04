using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected D_EnemyMoveState stateData;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    public EnemyMoveState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocityX(stateData.movementSpeed);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.SetVelocityX(stateData.movementSpeed);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
}
