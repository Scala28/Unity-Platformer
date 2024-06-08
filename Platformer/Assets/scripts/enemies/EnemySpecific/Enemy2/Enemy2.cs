using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    #region State variables
    public E2_IdleState IdleState { get; private set; }
    public E2_MoveState MoveState { get; private set; }
    public E2_AttackState AttackState { get; private set; }
    public E2_KnockoutState KnockoutState { get; private set; }
    public E2_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_EnemyIdleState idleStateData;
    [SerializeField]
    private D_EnemyMoveState moveStateData;
    [SerializeField]
    private D_EnemyAttackState attackStateData;
    [SerializeField]
    private D_EnemyKnockoutState knockoutStateData;
    [SerializeField]
    private D_EnemyDeadState deadStateData;
    #endregion

    [SerializeField]
    private Transform playerCheck;

    private Transform playerTransform;

    public override void Start()
    {
        base.Start();
        IdleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        MoveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        AttackState = new E2_AttackState(this, stateMachine, "attack", attackStateData, this);
        KnockoutState = new E2_KnockoutState(this, stateMachine, "knockout", knockoutStateData, this);
        DeadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine.Initialize(MoveState);
    }
    public bool CheckAttack()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, playerTransform.position - transform.position);
        float playerDistance = Vector2.Distance(transform.position, playerTransform.position);
        return info.collider.tag == "Player" && playerDistance <= attackStateData.AttackDistance && AttackState.CanAttack();
    }
    public override void Damage(float[] attackDetails)
    {
        base.Damage(attackDetails);
        if (canTakeDamage)
        {
            if (currentHealth > 0.0f)
            {
                stateMachine.ChangeState(KnockoutState);
            }
            else
                stateMachine.ChangeState(DeadState);
        }
    }
    public void AnimationFinished() => stateMachine.CurrentState.AnimationFinishedTrigger();
    public void AnimationDealDamage() => stateMachine.CurrentState.AnimationDealDamage();
    public bool CheckPlayerInAttackRange()
    {
        return Physics2D.OverlapCircle(playerCheck.position, attackStateData.AttackDistance, EntityData.WhatIsPlayer);
    }
    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(playerCheck.position, attackStateData.AttackDistance);
        
    }
}
