using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;

    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
}
