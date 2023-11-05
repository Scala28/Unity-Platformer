using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    #region State variables
    public E1_MoveState MoveState { get; private set; }
    public E1_IdleState IdleState { get; private set; }
    public E1_AttackState AttackState { get; private set; }
    public E1_KnockoutState KnockoutState { get; private set; }
    public E1_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_EnemyMoveState moveStateData;
    [SerializeField]
    private D_EnemyIdleState idleStateData;
    [SerializeField]
    private D_EnemyAttackState attackStateData;
    [SerializeField]
    private D_EnemyKnockoutState knockoutStateData;
    [SerializeField]
    private D_EnemyDeadState deadStateData;
    #endregion

    [SerializeField]
    private Transform playerCheck;

    public override void Start()
    {
        base.Start();
        MoveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        AttackState = new E1_AttackState(this, stateMachine, "attack", attackStateData, this);
        KnockoutState = new E1_KnockoutState(this, stateMachine, "knockout", knockoutStateData, this);
        DeadState = new E1_DeadState(this, stateMachine, "dead", deadStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public bool CheckPlayerInAttackRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, attackStateData.AttackDistance, EntityData.WhatIsPlayer);
    }
    public override void Damage(float[] attackDetails)
    {
        base.Damage(attackDetails);
        if (canTakeDamage)
        {
            if (currentHealth > 0.0f)
            {
                IdleState.SetFlipAfterIdle(false);
                stateMachine.ChangeState(KnockoutState);
            }
            else
                stateMachine.ChangeState(DeadState);
        }
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(transform.right * attackStateData.AttackDistance));
    }
}
