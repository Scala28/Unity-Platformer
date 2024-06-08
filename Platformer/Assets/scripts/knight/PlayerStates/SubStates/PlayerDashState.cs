using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; } = true;
    private float lastDashTime;

    private float dashTime;
    public bool IsDashing { get; private set; }

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public bool CheckCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.DashCoolDown;
    }
    public void ResetCanDash() => CanDash = true;

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        lastDashTime = Time.time;
        dashTime = playerData.DashTime;
        CanDash = false;
        IsDashing = true;
        if(player.DashGlow != null)
        {
            player.SetRendererMaterial(player.DashGlow);
        }
    }

    public override void Exit()
    {
        base.Exit();
        IsDashing = false;
        CanDash = true;
        if(player.DashGlow != null)
        {
            player.SetRendererMaterial(player.PrevRendererMaterial);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if(dashTime <= 0)
        {
            player.SetVelocityX(0);
            isAbilityDone = true;
        }
        else
        {
            dashTime -= Time.fixedDeltaTime;
            if(player.FacingRight)
            {
                player.SetVelocityX(playerData.DashSpeed);
            }
            else
            {
                player.SetVelocityX(-playerData.DashSpeed);
            }
            player.SetVelocityY(0);
        }
    }
}
