using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class patrolEnemy : MonoBehaviour
{
    [Header("patrol")]
    public float speed;
    private bool movingRight = true;
    public Transform groundDetec;
    private Transform player;
    public Transform wallDetec;
    public LayerMask whatIsWall;

    [Header("attack")]
    public float attackDistance;
    public bool canAttack;
    private float timetwShot;
    public float shootingTime;
    public GameObject slurtAttack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.queriesStartInColliders = false;
        timetwShot = 0;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D info = Physics2D.Raycast(transform.position, player.position - transform.position);
        Debug.DrawLine(transform.position, info.point);

        if (info.collider.tag == "Player" || info.collider.tag == "Bullet")
        {
            if(Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                canAttack = true;
            }
            else
            {
                canAttack = false;
            }
        }
        else
        {
            canAttack = false;
        }


        if (!canAttack)
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
            if(player.position.x > transform.position.x)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            if(timetwShot <= 0)
            {
                Instantiate(slurtAttack, transform.position, Quaternion.identity);
                timetwShot = shootingTime;
            }
            else
            {
                timetwShot -= Time.deltaTime;
            }
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


}
