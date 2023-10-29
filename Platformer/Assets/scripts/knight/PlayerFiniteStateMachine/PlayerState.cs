using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    protected bool isAnimationFinished;

    private string animBoolName;

    protected bool isExitingState;

    protected bool animMovementTrigger;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        startTime = Time.time;
        player.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger() { }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
    public virtual void AnimationStartMovementTrigger() => animMovementTrigger = true;
    public virtual void AnimationStopMovementTrigger() => animMovementTrigger = false;
}
