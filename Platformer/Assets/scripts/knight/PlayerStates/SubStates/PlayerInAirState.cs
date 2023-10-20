using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private float xInput;
    private bool isGrounded;
    private float jumpHoldTime;
    private bool jumpInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        jumpHoldTime = playerData.JumpHoldTime;
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
            stateMachine.ChangeState(player.JumpState);
        }
        else
        {
            player.CheckFlip(xInput);
            player.SetVelocityX(playerData.MovementSpeed * xInput * playerData.InAirMovementControl);

            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        }
    }
    private void HoldJump()
    {
        if (player.InputHandler.JumpHoldInput && player.JumpState.FirstJump()) 
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
