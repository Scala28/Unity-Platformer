using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyFiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    protected string animBoolName;

    protected bool isAnimationFinished;

    public EnemyState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        startTime = Time.time;
        entity.Anim.SetBool(animBoolName, true);
        isAnimationFinished = false;
    }
    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;
    public virtual void AnimationDealDamage() { }
}
