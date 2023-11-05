using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    protected D_EnemyDeadState stateData;
    public EnemyDeadState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyDeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.Die();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
