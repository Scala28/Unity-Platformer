using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy4 : Entity
{
    #region State variables
    public E4_MoveState MoveState { get; private set; }
    public E4_KnockoutState KnockoutState { get; private set; }
    public E4_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_EnemyMoveState moveStateData;
    [SerializeField]
    private D_EnemyKnockoutState knockoutStateData;
    [SerializeField]
    private D_EnemyDeadState deadStateData;
    #endregion

    #region Pathfinding
    [Header("Pathfinding stats")]
    [SerializeField]
    private Transform target;

    public int nextWayPointDistance;

    public Path path { get; private set; }
    public int currentWayPoint { get; private set; }

    private bool endOfPath;

    Seeker seeker;
    #endregion

    public override void Start()
    {
        base.Start();
        MoveState = new E4_MoveState(this, stateMachine, "move", moveStateData, this);
        KnockoutState = new E4_KnockoutState(this, stateMachine, "knockout", knockoutStateData, this);
        DeadState = new E4_DeadState(this, stateMachine, "dead", deadStateData, this);

        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        if (!TryGetComponent<Seeker>(out seeker))
            Debug.LogError("Missing Seeker component for pathfinding!");

        currentWayPoint = 0;
        InvokeRepeating("UpdatePath", 0f, .5f);

        stateMachine.Initialize(MoveState);
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(RB.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    public void AddForceToRB(Vector2 movement)
    {
        RB.AddForce(movement);
    }
    public void IncrementCurrentWayPoint() => currentWayPoint++;

    public override void Damage(float[] attackDetails)
    {
        base.Damage(attackDetails);
        if (canTakeDamage)
        {
            if (currentHealth > 0.0f)
            {
                stateMachine.ChangeState(KnockoutState);
            }
            else
                stateMachine.ChangeState(DeadState);
        }
    }

    public override void OnDrawGizmosSelected()
    {
        //base.OnDrawGizmosSelected();
    }
}
