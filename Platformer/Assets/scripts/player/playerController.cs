using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed;

    private float moveInput;
    
    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool canMove;


    [Header("Jump")]
    public Transform groundCeck;
    public float ceckRadius;
    public LayerMask whatIsGround;
    private bool isGrounded;
    
    public float jumpForce;
    private bool canJump;

    [Header("extra Jump")]
    public int JumpValue;
    private int extraJump;

    public int extraJumpStamina;

    [Header("dash")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    public int dashStamina;
    private bool canDash;
    public float timeBtwDash;
    private bool isDashing;

    [Header("stamina")]
    public int maxStamina = 100;
    private int currentStamina;
    public staminaBar staminaBar;
    public float regenSpeed = 0.05F;
    public float timeBfRegen = 1.5f;
    private Coroutine regen;

   [Header("Wall")]
    public Transform frontCeck;
    bool isTouchingFront;
    bool wallSliding;
    public float wallSlidingSpeed;
    public LayerMask whatIsWall;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
        extraJump = JumpValue;
        dashTime = startDashTime;
        canDash = true;
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
    }

    

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCeck.position, ceckRadius, whatIsGround);

        isTouchingFront = Physics2D.OverlapCircle(frontCeck.position, ceckRadius, whatIsWall);

        if(canMove)
        {
            moveInput = Input.GetAxis("Horizontal");
        }


        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        

        if (isGrounded == true)
        {
            extraJump = JumpValue;
        }

        if(canJump)
        {
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else if (Input.GetButtonDown("Jump") && isGrounded == false && !wallSliding && extraJump > 0 && currentStamina >= extraJumpStamina)
            {
                rb.velocity = Vector2.up * jumpForce;
                extraJump--;

                RemoveStamina(extraJumpStamina);
                if (regen != null)
                {
                    StopCoroutine(regen);
                }
                regen = StartCoroutine(RegenStamina());
            }
        }

        if (isTouchingFront == true && isGrounded == false && moveInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if(wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            extraJump = JumpValue;
        }

        if(Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if(wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * - moveInput, yWallForce);
        }


        if (direction == 0 && currentStamina >= dashStamina && canDash)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(facingRight == false)
                {
                    direction = 1;
                }
                else
                {
                    direction = 2;
                }
                canDash = false;
                RemoveStamina(dashStamina);
                if(regen != null)
                {
                    StopCoroutine(regen);
                }
                regen = StartCoroutine(RegenStamina());
            }  
        }
        else if(direction != 0)
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                isDashing = false;
                Invoke("SetCanDashToTrue", timeBtwDash);
            }
            else
            {
                dashTime -= Time.deltaTime;
                isDashing = true;
                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if(direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
            }
        }
        
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
  

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    void SetCanDashToTrue()
    {
        canDash = true;
    }
    

    void RemoveStamina(int stamina)
    {
        currentStamina -= stamina;
        staminaBar.SetStamina(currentStamina);
    }

  IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeBfRegen);
        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.SetStamina(currentStamina);

            yield return new WaitForSeconds(regenSpeed);
        }
        regen = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCeck.position, ceckRadius);

        Gizmos.DrawWireSphere(frontCeck.position, ceckRadius);
    }


}

