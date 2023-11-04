using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttackState : PlayerAbilityState
{
    private int animCounter;
    private int[] attackAnim;

    private bool attackStreak;
    private float lastAttackTime;

    private float[] attackDetails = new float[3];
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


    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackStreak = Time.time < lastAttackTime + playerData.AttackStreakTime;
        if (player.AttackGlow != null)
        {
            player.SetRendererMaterial(player.AttackGlow);
        }
        if (!attackStreak)
        {
            animCounter = 0;
        }
        else
        {
            animCounter++;
            if (animCounter >= attackAnim.Length)
            {
                animCounter = animCounter % attackAnim.Length;
            }
        }
        player.Anim.SetFloat("attackAnim", (float)animCounter);
    }

    public override void Exit()
    {
        base.Exit();
        if (player.AttackGlow != null)
        {
            player.SetRendererMaterial(player.PrevRendererMaterial);
        }
        lastAttackTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.SetVelocityX(playerData.MovementSpeed * xInput);
        if (animMovementTrigger)
        {
            player.SetVelocityX(playerData.AttackMovementSpeed * player.FacingDirection());
            player.SetVelocityY(0f);
        }
        else
        {
            player.SetVelocityX(0f);
            player.CheckFlip(xInput);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void DealDamage()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(player.GetAttackPosition(), playerData.SwordRadius, playerData.WhatIsDamageable);
        attackDetails[0] = playerData.SwordDamage;
        attackDetails[1] = player.transform.position.x;
        attackDetails[2] = player.transform.position.y;
        foreach (Collider2D collider in detectedObjs)
        {
            collider.transform.SendMessage("Damage", attackDetails);
            //TODO: instantiate hit particle
        }
    }
}
public enum AttackAnimation
{
    swordOne,
    //swordTwo
}