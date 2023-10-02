using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("patrolling")]
    public float speed;
    private bool movingRight = true;
    public Transform groundDetec;
    private Transform player;
    private bool canMove;
    public LayerMask whatIsWall;
    public Transform wallDetec;

    [Header("attack")]
    public float attackSpeed;
    public float attackDistance;
    private bool canAttack;
    public bool isAttacking;
    public float attackDuration;
    private float attackTime;
    public float timeBtwAttack;
    public int touchDamage;

    [Header("Anim")]
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.queriesStartInColliders = false;
        rb = GetComponent<Rigidbody2D>();
        attackTime = attackDuration;
        canMove = true;
        canAttack = false;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(!canAttack)
        {
            RaycastHit2D info = Physics2D.Raycast(transform.position, transform.right, attackDistance);
            if (info.collider != null)
            {
                Debug.DrawLine(transform.position, info.point);
                if (info.collider.tag == "Player")
                {
                    canAttack = true;
                    canMove = false;
                }
            }
        }

        if(canMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetec.position, Vector2.down, 2f);
            RaycastHit2D wallInfo = Physics2D.Raycast(wallDetec.position, transform.right, 0.5f, whatIsWall);
            if (groundInfo.collider == false || wallInfo.collider == true)
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
        }
        if(canAttack && !canMove)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetec.position, Vector2.down, 2f);
            RaycastHit2D wallInfo = Physics2D.Raycast(wallDetec.position, transform.right, 0.5f, whatIsWall);

            if (attackTime <= 0 || groundInfo.collider == false || wallInfo.collider == true)
            {
                canMove = true;
                StartCoroutine(SetCAnAttackToFalse());
                attackTime = attackDuration;
                rb.velocity = Vector2.zero;
                isAttacking = false;
            }
            else
            {
                attackTime -= Time.deltaTime;
                isAttacking = true;
                if(movingRight)
                {
                    rb.velocity = Vector2.right * attackSpeed;
                }
                else
                {
                    rb.velocity = Vector2.left * attackSpeed;
                }
            }
        }

        anim.SetBool("isAttacking", isAttacking);
    
    }

    IEnumerator SetCAnAttackToFalse()
    {
        yield return new WaitForSeconds(timeBtwAttack);
        canAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            int direction = 0;
            if(transform.position.x < player.position.x)
            {
                direction = -1;
            }
            else if(transform.position.x > player.position.x)
            {
                direction = 1;
            }
            collision.GetComponent<Health>().TakeDamage(touchDamage, direction);
        }
    }

}
