using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variable
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator Anim { get; private set; }

    public SpriteRenderer Renderer { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Check Transforms
    [Header("Transform checks")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    #endregion

    #region Shader effects
    [Header("Shader effects")]
    public Material WingsGlow;
    public Material PrevRendererMaterial { get; private set; }

    #endregion

    #region Others
    [Header("Others")]
    public bool ShowGiwmos;
    public Vector2 CurrentVelocity { get; private set; }
    public bool FacingRight { get; private set; }


    private Vector2 workSpace;
    #endregion

    #region Unity callbacks

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "wallJump");
    }
    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();

        StateMachine.Initialize(IdleState);
        FacingRight = true;
        CameraManager.instance.CallCameraFaceDirection();
    }
    private void Update()
    {
        CurrentVelocity = RB.velocity;
        StateMachine.CurrentState.DoChecks();
        StateMachine.CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set functions
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
    public void SetVelocityY(float velocity)
    {
        workSpace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * direction * velocity, angle.y * velocity);
        RB.velocity = workSpace;
        CurrentVelocity = workSpace;
    }
    public void SetRendererMaterial(Material mat)
    {
        PrevRendererMaterial = Renderer.material;
        Renderer.material = mat;
    }
    #endregion

    #region Check functions

    public bool CheckGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.GroundCheckRadius, playerData.WhatIsGround);
    }
    public bool CheckTouchingWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, playerData.WallCheckRadius, playerData.WhatIsWall);
    }
    public void CheckFlip(float xInput)
    {
        if(FacingRight && xInput < 0)
        {
            Flip();
        }else if(!FacingRight && xInput > 0)
        {
            Flip();
        }
    }
    #endregion

    #region Other functions
    public int FacingDirection()
    {
        return FacingRight ? 1 : -1;
    }
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);

        CameraManager.instance.CallCameraFaceDirection();
    }
    public void FlipSprite()
    {
        Renderer.flipX = !Renderer.flipX;
    }

    private void OnDrawGizmosSelected()
    {
        if (ShowGiwmos)
        {
            Gizmos.DrawWireSphere(groundCheck.position, playerData.GroundCheckRadius);
            Gizmos.DrawWireSphere(wallCheck.position, playerData.WallCheckRadius);
        }
    }
    #endregion
}
