using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {
        jumpsLeft = playerData.AmountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityY(playerData.JumpForce);
        jumpsLeft--;
        if (FirstJump())
        {
            isAbilityDone = true;
        }
        else
        {
            player.Anim.SetBool("wings", true);
            if(player.WingsGlow != null)
            {
                player.SetRendererMaterial(player.WingsGlow);
            }
        }

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!FirstJump())
        {
            if (isAnimationFinished)
            {
                isAbilityDone = true;
            }
        }
        player.CheckFlip(player.InputHandler.RawMovementInput.x);
        player.SetVelocityX(playerData.MovementSpeed * xInput * playerData.InAirMovementControl);
    }
    public override void Exit()
    {
        base.Exit();
        if (!FirstJump())
        {
            player.Anim.SetBool("wings", false);
            if(player.WingsGlow != null)
            {
                player.SetRendererMaterial(player.PrevRendererMaterial);
            }
        }
    }
    public bool CanJump()
    {
        return jumpsLeft > 0 ? true : false;
    }
    public void ResetJumpsLeft() => jumpsLeft = playerData.AmountOfJumps;
    public void DecreaseJumpsLeft() => jumpsLeft--;
    public bool FirstJump()
    {
        return jumpsLeft == playerData.AmountOfJumps - 1;
    }
}
