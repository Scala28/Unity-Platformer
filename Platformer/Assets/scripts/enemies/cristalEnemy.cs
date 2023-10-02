using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalEnemy : MonoBehaviour
{
    [Header("Patrolling")]
    public float speed;
    public Transform groundDetec;
    public Transform wallDetec;
    public LayerMask whatIsWall;
    private bool movingRight;

    [Header("Attack")]
    public float attackDistance;
    private Transform player;
    public bool attack;
    private bool canAttack;
    public float attackTime;
    public LayerMask whatIsPlayer;
    public int damage;
    public float attackDelay;

    private Rigidbody2D rb;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.queriesStartInColliders = false;
        attack = false;
        canAttack = true;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, player.position - transform.position);
        Debug.DrawLine(transform.position, info.point);

        if(info.collider.tag == "Player" && Vector2.Distance(transform.position, player.position) <= attackDistance && canAttack == true)
        {
            attack = true;
        }

        if(!attack)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetec.position, Vector2.down, 2f);
            RaycastHit2D wallInfo = Physics2D.Raycast(wallDetec.position, transform.right, 0.5f, whatIsWall);

            if (groundInfo.collider == false || wallInfo.collider == true)
            {
                Turn();
            }
            else
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetTrigger("attack");
            attack = false;
            canAttack = false;
            Invoke("Attack", attackDelay);
            Invoke("SetCanAttackToTrue", attackTime);
        }
    }
    void Turn()
    {
        if (movingRight == true)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
        }
    }
    void Attack()
    {
        bool hitPlayer = Physics2D.OverlapCircle(transform.position, attackDistance, whatIsPlayer);
        if(hitPlayer)
        {
            int direction = 0;
            if(player.position.x > transform.position.x)
            {
                direction = -1;
            }
            else if(player.position.x < transform.position.x)
            {
                direction = 1;
            }
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().TakeDamage(damage, direction);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

    void SetCanAttackToTrue()
    {
        canAttack = true;
    }
}
