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
    private Vector2 velocityWorkspace;
    #region Unity callbacks
    public virtual void Start()
    {
        stateMachine = new EnemyFiniteStateMachine();
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
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
        velocityWorkspace.Set(FacingDirection * velocity, RB.velocity.y);
        RB.velocity = velocityWorkspace;
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
    public virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(transform.right * EntityData.WallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * EntityData.LedgeCheckDistance));
    }
    #endregion
}
