using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected float xInput;
    private bool JumpInput;
    private bool dashInput;
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) 
        : base(player, stateMachine, playerData, animBoolName)
    {

    }
    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.InputX;
        JumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput; 

        if (player.InputHandler.AttackInputs[(int)CombatInputs.sword] && player.AttackState.CanAttack())
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if (JumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.UseJump();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!player.CheckGrounded())
        {
            player.JumpState.DecreaseJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }
        else if (dashInput && player.DashState.CheckCanDash())
        {
            player.InputHandler.UseDashInput();
            player.JumpState.DecreaseJumpsLeft();
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
