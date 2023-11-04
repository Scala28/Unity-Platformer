using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BeePathfinderController : MonoBehaviour
{
    private enum State
    {
        Moving,
        KnockBack,
        Dead
    }
    private State currentState;

    public Transform Target { get; private set; }

    [SerializeField]
    private float
        movementSpeed,
        maxHealth,
        knockBackDuration;
    [SerializeField]
    private Vector2 knockbackSpeed;

    private bool facingLeft;
    private Vector2 movement;
    private float
        currentHealth,
        knockBackStartTime;
    private Vector2 damageDirection;


    [Header("pathfinder seeker")]
    [SerializeField]
    private int nextWayPointDistance = 3;

    Path path;
    int currentWayPoint = 0;
    bool endOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        Target = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        if (!TryGetComponent<Seeker>(out seeker))
            Debug.LogError("Missing Seeker component for pathfinding!");

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        facingLeft = true;
        currentHealth = maxHealth;

        InvokeRepeating("UpdatePath", 0f, .5f);

        currentState = State.Moving;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.KnockBack:
                UpdateKnockBackState();
                break;
        }
    }

    #region Moving state
    private void EnterMovingState()
    {

    }
    private void ExitMovingState()
    {
        
    }
    private void UpdateMovingState()
    {
        if (path == null)
            return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            endOfPath = true;
            return;
        }
        else
        {
            endOfPath = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        movement = direction * movementSpeed;

        rb.AddForce(movement);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWayPointDistance)
        {
            currentWayPoint++;
        }

        if (rb.velocity.x <= -0.1f && facingLeft == false)
        {
            Flip();
        }
        else if (rb.velocity.x >= 0.01f && facingLeft == true)
        {
            Flip();
        }
    }
    #endregion

    #region Knockback state
    private void EnterKnockBackState()
    {
        anim.SetBool("knockBack", true);
        knockBackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection.x, knockbackSpeed.y * damageDirection.y);
        rb.velocity = movement;
    }
    private void ExitKnockBackState()
    {
        anim.SetBool("knockBack", false); 
    }
    private void UpdateKnockBackState()
    {
        if (Time.time >= knockBackStartTime + knockBackDuration)
        {
            rb.velocity = Vector2.zero;
            SwitchState(State.Moving);
        }
    }
    #endregion

    #region Dead state
    private void EnterDeadState()
    {
        //TODO: Spawn particles
        Destroy(this.gameObject);
    }
    #endregion

    #region Other funcions
    private void SwitchState(State toState)
    {
        switch (currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.KnockBack:
                ExitKnockBackState();
                break;
        }
        switch (toState)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.KnockBack:
                EnterKnockBackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        currentState = toState;
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, Target.position, OnPathComplete);
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
    private void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        if (attackDetails[1] > transform.position.x)
            damageDirection.x = -1;
        else damageDirection.x = 1;
        if (attackDetails[2] > transform.position.y)
            damageDirection.y = -1;
        else damageDirection.y = 1;

        //TODO: Spawn particles

        if (currentHealth > 0.0f)
        {
            SwitchState(State.KnockBack);
        }
        else SwitchState(State.Dead);
    }
    #endregion

}
