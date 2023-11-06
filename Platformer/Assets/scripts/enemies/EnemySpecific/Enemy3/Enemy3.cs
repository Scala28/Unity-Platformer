using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    #region State variables
    public E3_IdleState IdleState { get; private set; }
    public E3_MoveState MoveState { get; private set; }
    public E3_AttackState AttackState { get; private set; }
    public E3_KnockoutState KnockoutState { get; private set; }
    public E3_DeadState DeadState { get; private set; }

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

    private Transform playerTransform;

    [SerializeField]
    private GameObject bullet;

    public override void Start()
    {
        base.Start();
        IdleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        MoveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        AttackState = new E3_AttackState(this, stateMachine, "attack", attackStateData, this);
        KnockoutState = new E3_KnockoutState(this, stateMachine, "knockout", knockoutStateData, this);
        DeadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        Physics2D.queriesStartInColliders = false;

        stateMachine.Initialize(MoveState);
    }
    public override void SetVelocityX(float velocity)
    {
        base.SetVelocityX(velocity);
    }
    public bool CheckAttack()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, playerTransform.position - transform.position);

        if (info.collider == null)
            return true;
        bool inRange = Vector2.Distance(transform.position, playerTransform.position) <= attackStateData.AttackDistance;

        return (info.collider.tag == "Player" || info.collider.tag == "Bullet") && inRange;
    }
    public void FacePlayer()
    {
        if (transform.position.x > playerTransform.position.x && FacingDirection == 1)
            Flip();
        else if (transform.position.x < playerTransform.position.x && FacingDirection == -1)
            Flip();
    }
    public void Attack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
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
}
