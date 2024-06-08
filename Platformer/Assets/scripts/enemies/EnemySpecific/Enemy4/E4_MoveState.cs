using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_MoveState : EnemyMoveState
{
    private Enemy4 enemy;
    public E4_MoveState(Entity entity, EnemyFiniteStateMachine stateMachine, string animBoolName, D_EnemyMoveState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        //base.Enter();
    }
    public override void LogicUpdate()
    {
        //base.LogicUpdate();
        if (enemy.path == null || enemy.currentWayPoint >= enemy.path.vectorPath.Count)
            return;
        Vector2 direction = ((Vector2)enemy.path.vectorPath[enemy.currentWayPoint] - enemy.RB.position).normalized;
        enemy.AddForceToRB(direction * stateData.movementSpeed);

        float distance = Vector2.Distance(enemy.RB.position, enemy.path.vectorPath[enemy.currentWayPoint]);
        if (distance < enemy.nextWayPointDistance)
            enemy.IncrementCurrentWayPoint();

        if (enemy.RB.velocity.x <= -.1f && enemy.FacingDirection == 1)
            enemy.Flip();
        else if (enemy.RB.velocity.x >= .1f && enemy.FacingDirection == -1)
            enemy.Flip();
    }

    public override void PhysicsUpdate()
    {
        //base.PhysicsUpdate();
    }
}
