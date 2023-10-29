using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private int animCounter;
    private int[] attackAnim;

    private bool attackStreak;
    private float lastAttackTime;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        int count = Enum.GetValues(typeof(AttackAnimation)).Length;
        attackAnim = new int[count];
        animCounter = 0;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAbilityDone = true;
    }

    public override void AnimationStartMovementTrigger()
    {
        base.AnimationStartMovementTrigger();
    }

    public override void AnimationStopMovementTrigger()
    {
        base.AnimationStopMovementTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackStreak = Time.time < lastAttackTime + playerData.AttackStreakTime;
        if(player.AttackGlow != null)
        {
            player.SetRendererMaterial(player.AttackGlow);
        }
        if(!attackStreak)
        {
            animCounter = 0;
        }
        else
        {
            animCounter++;
            if(animCounter >= attackAnim.Length)
            {
                animCounter = animCounter % attackAnim.Length;
            }
        }
        player.Anim.SetFloat("attackAnim", (float)animCounter);
    }

    public override void Exit()
    {
        base.Exit();
        if(player.AttackGlow != null)
        {
            player.SetRendererMaterial(player.PrevRendererMaterial);
        }
        lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.CheckFlip(xInput);
        player.SetVelocityX(playerData.MovementSpeed * xInput);
        if (animMovementTrigger)
        {
            player.SetVelocityX(playerData.AttackMovementSpeed * player.FacingDirection());
        }
        else { player.SetVelocityX(0f); }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
public enum AttackAnimation
{
    swordOne,
    swordTwo
}
