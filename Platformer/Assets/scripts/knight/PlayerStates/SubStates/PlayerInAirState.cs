using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float xInput;
    private bool isGrounded, isTouchingWall;
    private float jumpHoldTime;
    private bool jumpInput;
    private bool canHoldJump;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
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
        jumpHoldTime = playerData.JumpHoldTime;
        canHoldJump = player.JumpState.FirstJump() && stateMachine.PrevState != player.WallJumpState;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        HoldJump();
        xInput = player.InputHandler.InputX;
        jumpInput = player.InputHandler.JumpInput;
        if(isGrounded && player.CurrentVelocity.y < .01f)
        {
            player.Anim.SetFloat("yVelocity", 0f);
            stateMachine.ChangeState(player.LandState);
        }
        else if(jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJump();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall)
        {
            if(((player.InputHandler.RawMovementInput.x > 0 && player.FacingDirection() == 1) || 
                (player.InputHandler.RawMovementInput.x < 0 && player.FacingDirection() == -1 )) && !(player.CurrentVelocity.y > 0))
            {
                stateMachine.ChangeState(player.WallSlideState);

            }
        }
        else
        {
            player.CheckFlip(player.InputHandler.RawMovementInput.x);
            player.SetVelocityX(playerData.MovementSpeed * xInput * playerData.InAirMovementControl);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        }
    }
    private void HoldJump()
    {
        if (player.InputHandler.JumpHoldInput && canHoldJump) 
        {
            if (jumpHoldTime > 0)
            {
                player.SetVelocityY(playerData.JumpForce);
                jumpHoldTime -= Time.deltaTime;
            }
            else
            {
                player.InputHandler.UseJumpHold();
            }
        }
        else { jumpHoldTime = 0; }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
