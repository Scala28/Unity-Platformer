using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slurtBullet : MonoBehaviour
{
    public float speed;
    private Transform player;

    public float lifeTime;
    private float lifeCounter;

    public GameObject particle;

    public int bulletDamage;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestoyBullet();

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
            collision.GetComponent<Health>().TakeDamage(bulletDamage, direction);
        }
    }

    void DestoyBullet()
    {
        Destroy(gameObject);
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
