using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_MoveState MoveState { get; private set; }
    public E1_IdleState IdleState { get; private set; }
    public E1_AttackState AttackState { get; private set; }

    [SerializeField]
    private D_EnemyMoveState moveStateData;
    [SerializeField]
    private D_EnemyIdleState idleStateData;
    [SerializeField]
    private D_EnemyAttackState attackStateData;

    [SerializeField]
    private Transform playerCheck;

    public override void Start()
    {
        base.Start();
        MoveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        AttackState = new E1_AttackState(this, stateMachine, "attack", attackStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public bool CheckPlayerInAttackRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, attackStateData.AttackDistance, EntityData.WhatIsPlayer);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(transform.right * attackStateData.AttackDistance));
    }
}
