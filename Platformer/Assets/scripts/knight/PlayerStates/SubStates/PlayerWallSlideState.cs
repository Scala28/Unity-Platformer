using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    private bool isGrounded, isTouchingWall;
    private float xInput;
    private bool jumpInput;
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckGrounded();
        isTouchingWall = player.CheckTouchingWall();
    }

    public override void Enter()
    {
        base.Enter();
        player.FlipSprite();
        player.JumpState.ResetJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
        player.FlipSprite();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.RawMovementInput.x;
        jumpInput = player.InputHandler.JumpInput;
        //player.CheckFlip(xInput);
        if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }else if(!isTouchingWall || ((xInput > 0 && player.FacingDirection() != 1) ||
                (xInput < 0 && player.FacingDirection() != -1)) || xInput == 0)
        {
            player.JumpState.DecreaseJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (jumpInput)
        {
            player.InputHandler.UseJump();
            stateMachine.ChangeState(player.WallJumpState);
        }
        player.SetVelocityY(Mathf.Clamp(player.CurrentVelocity.y, -playerData.WallSlideSpeed, float.MaxValue));

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
