using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    [SerializeField]
    private float 
        speed,
        lifeTime;
    [SerializeField]
    private GameObject particle;

    private float lifeCounter;
    private Transform player;
    private bool facingRight = false;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        lifeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if(lifeCounter <= 0)
        {
            DestoyBullet();
        }
        else
        {
            lifeCounter -= Time.deltaTime;
        }

        

        if(player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if(player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestoyBullet();

        if (collision.collider.CompareTag("Player"))
        {
            //TODO: Hurt the player
        }
    }

    private void DestoyBullet()
    {
        Destroy(gameObject);
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    public void Damage(float[] attackDetails)
    {
        Debug.Log("Destroyed");
        DestoyBullet();
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
