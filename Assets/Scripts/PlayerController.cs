using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 3f;
    public float apexHeight = 10f;
    public float apexTime = 10f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private float gravity;
    private float jumpVelocity;
    private float upVelocity;
    public float terminalVelocity = 8;
    public float coyoteTime = 0.3f;
    private float timeFromGround;
    public enum FacingDirection
    {
        left, right
    }
    private FacingDirection facingDirection = FacingDirection.right;

    void Start()
    {
        CalcJumping();
    }


    void Update()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        MovementUpdate(playerInput);
        UpdateFacingDirection(playerInput);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (isJumping) {
            CurrentJump();
        }
    }

    void CalcJumping()
    {
        gravity = -2 * apexHeight / (apexTime * apexTime);
        jumpVelocity = 2 * apexHeight / apexTime;

    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (playerInput.x != 0)
        {
            rb.AddForce(new Vector2(playerInput.x * acceleration, 0), ForceMode2D.Force);

            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }
        else if (rb.velocity.magnitude > 0.15f)
        {
            rb.AddForce(new Vector2(-rb.velocity.x * deceleration, 0), ForceMode2D.Force);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void Jump()
    {
        if (isGrounded || Time.time - timeFromGround <= coyoteTime)
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            upVelocity = jumpVelocity;
        }
    }

    private void CurrentJump() {
        upVelocity += gravity * Time.deltaTime;
        rb.velocity = new Vector2(rb.velocity.x, upVelocity);

        if (upVelocity < -terminalVelocity)
        {
            upVelocity = -terminalVelocity;
        }

        if (upVelocity <= 0 && rb.velocity.y <= 0)
        {
            isJumping = false;
        }
    }


    private void UpdateFacingDirection(Vector2 input)
    {
        if (input.x > 0 && facingDirection == FacingDirection.right)
        {
            facingDirection = FacingDirection.left;
            Flip();
        }
        else if (input.x < 0 && facingDirection == FacingDirection.left)
        {
            facingDirection = FacingDirection.right;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsWalking()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            return true;
        }else { return false; }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            timeFromGround = Time.time;
        }
    }

    public bool IsGrounded()
    {
        if (isGrounded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
