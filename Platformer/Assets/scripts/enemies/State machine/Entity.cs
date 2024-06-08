using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public D_Entity EntityData;
    public EnemyFiniteStateMachine stateMachine { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;

    public int FacingDirection { get; private set; } = 1;

    protected Vector2 damageDirection;
    protected bool canTakeDamage;
    protected float currentHealth;
    private Vector2 velocityWorkspace;
    private DamageFlasher damageFlasher;
    private bool canDamageFlash;

    #region Unity callbacks
    public virtual void Start()
    {
        stateMachine = new EnemyFiniteStateMachine();
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        canDamageFlash = TryGetComponent<DamageFlasher>(out damageFlasher);
        currentHealth = EntityData.MaxHealth;
        canTakeDamage = true;
    }
    public virtual void Update()
    {
        stateMachine.CurrentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region set funcions
    public virtual void SetVelocityX(float velocity)
    {
        velocityWorkspace.Set(velocity, RB.velocity.y);
        RB.velocity = velocityWorkspace;
    }
    public virtual void SetVelocityY(float velocity)
    {
        velocityWorkspace.Set(RB.velocity.x, velocity);
        RB.velocity = velocityWorkspace;
    }
    public virtual void SetCanTakeDamage(bool canTakeDamage)
    {
        this.canTakeDamage = canTakeDamage;
    }
    #endregion

    #region Check funcions
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, EntityData.WallCheckDistance, EntityData.WhatIsWall);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, EntityData.LedgeCheckDistance, EntityData.WhatIsGround);
    }
    #endregion

    #region Other functions
    public virtual void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
    public virtual void Damage(float[] attackDetails)
    {
        if (canTakeDamage)
        {
            currentHealth -= attackDetails[0];
            damageDirection.Set(0f, 0f);
            if (attackDetails[1] > transform.position.x + EntityData.KnockoutDirectionOffset.x)
                damageDirection.x = -1f;
            else if(attackDetails[1] < transform.position.x - EntityData.KnockoutDirectionOffset.x) damageDirection.x = 1f;

            if (attackDetails[2] > transform.position.y + EntityData.KnockoutDirectionOffset.y)
                damageDirection.y = -1f;
            else if(attackDetails[2] < transform.position.y - EntityData.KnockoutDirectionOffset.y) damageDirection.y = 1f;

            if(EntityData.DamageParticle != null)
                Instantiate(EntityData.DamageParticle, transform.position, Quaternion.identity);
        }
    }
    public virtual void CallDamageFlash()
    {
        if (canDamageFlash)
        {
            damageFlasher.Flash();
        }
    }
    public virtual Vector2 GetDamageDirection()
    {
        return damageDirection;
    }
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(transform.right * EntityData.WallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * EntityData.LedgeCheckDistance));
    }
    #endregion
}
