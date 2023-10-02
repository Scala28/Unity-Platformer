using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("movement")]
    public float speed;
    private float moveInput;
    private bool facingRight;

    [Header("jump")]
    public Transform groundCeck;
    public float ceckRadious;
    public LayerMask whatIsGround;
    private bool isGrounded;

    public float jumpForce;
    private float jumpTime;
    public float startJumpTime;
    private bool isjumping;

    public bool canExtraJump;
    public int startExtraJump;
    private int extraJump;
    public float extraJumpForce;

    [Header("dash")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool canDash;
    public bool isDashing;
    public float timeBtwDash;

    [Header("wall")]
    public Transform wallCeck;
    public LayerMask whatIsWall;
    private bool isTouchingFront;
    private bool wallSliding;
    public float wallSlidingSpeed;

    //wallJump
    public float wallJumpForce;
    private float wallJumpDirection = -1;
    public Vector2 wallJumpAngle;

    [Header("stamina")]
    public staminaBar staminaBar;
    public int maxStamina;
    private int currentStamina;
    private Coroutine regen;
    public float timeBfRegen;
    public float regenSpeed;
    public int dashStamina;
    public int extraJumpStamina;

    [Header("knockBack")]
    public float knockBackDuration;
    private float knockBackTime;
    bool knockBack;
    public Vector2 knockBAckSpeed;

    [Header("Animations")]
    public Animator anim;
    /*1. run
     2. idle
     3. jump
     4. extra jump
     5. dash
     6. wall slide
     7. attack
     8. hurt*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;

        extraJump = startExtraJump;
        canExtraJump = true;

        direction = 0;
        dashTime = startDashTime;
        canDash = true;
        isDashing = false;

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        knockBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        CeckStatements();
        
        CeckInput();

        if(wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            extraJump = startExtraJump;
        }

        if(moveInput < 0 && facingRight && !knockBack)
        {
            Flip();
        }
        else if(moveInput > 0 && !facingRight && !knockBack)
        {
            Flip();
        }

        SetAnim();
        CeckKnockBack();
    }

    void CeckInput()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(Input.GetButtonDown("Jump") && isGrounded &&!knockBack)
        {
            rb.velocity = Vector2.up * jumpForce;
            isjumping = true;
        }else if(Input.GetButtonDown("Jump") && !isGrounded && !wallSliding && extraJump > 0 && canExtraJump && currentStamina >= extraJumpStamina && !knockBack)
        {
            rb.velocity = Vector2.up * extraJumpForce;
            extraJump--;
            RemoveStamina(extraJumpStamina);
            isjumping = false;
        }

        if(Input.GetButton("Jump") && isjumping)
        {
            if(jumpTime > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isjumping = true;
            }
        }
        if(Input.GetButtonUp("Jump"))
        {
            isjumping = false;
        }
        if(Input.GetButtonDown("Jump") && wallSliding)
        {
            //wallJumping
            WallJump();
            
        }

        if (direction == 0 && canDash && currentStamina >= dashStamina && !knockBack)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (!facingRight)
                {
                    direction = 1;
                }
                else
                {
                    direction = 2;
                }
                RemoveStamina(dashStamina);
                canDash = false;
            }
        }
    }
    void WallJump()
    {
        //rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
        rb.velocity = new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y);
    }

    public void KnockBack(int direction)
    {
        knockBack = true;
        knockBackTime = Time.time;
        rb.velocity = new Vector2(knockBAckSpeed.x * -direction, knockBAckSpeed.y);
    }
    void CeckKnockBack()
    {
        if(Time.time > knockBackTime + knockBackDuration && knockBack)
        {
            knockBack = false;
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }

    void CeckStatements()
    {
        isGrounded = Physics2D.OverlapCircle(groundCeck.position, ceckRadious, whatIsGround);
        if (isGrounded)
        {
            jumpTime = startJumpTime;
            extraJump = startExtraJump;
        }

        isTouchingFront = Physics2D.OverlapCircle(wallCeck.position, ceckRadious, whatIsWall);
        if (!isGrounded && isTouchingFront && moveInput != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        
    }

    void SetAnim()
    {
        anim.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("wallSliding", wallSliding);
    }

    private void FixedUpdate()
    {
        if(!knockBack)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }


        if(direction != 0)
        {
            if(dashTime <= 0)
            {
                direction = 0;
                rb.velocity = Vector2.zero;
                dashTime = startDashTime;
                Invoke("SetCanDashToTrue", timeBtwDash);
                isDashing = false;
            }
            else
            {
                dashTime -= Time.fixedDeltaTime;
                if(direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                }
                else if(direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                isDashing = true;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        wallJumpDirection *= -1;
    }
    void RemoveStamina(int stamina)
    {
        currentStamina -= stamina;
        staminaBar.SetStamina(currentStamina);

        if (regen != null)
        {
            StopCoroutine(regen);
        }
        regen = StartCoroutine(RegenStamina());
    }

    IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(timeBfRegen);
        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.SetStamina(currentStamina);

            yield return new WaitForSeconds(regenSpeed);
        }
        regen = null;
    }

    void SetCanDashToTrue()
    {
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCeck.position, ceckRadious);
        Gizmos.DrawWireSphere(wallCeck.position, ceckRadious);
    }
}

   