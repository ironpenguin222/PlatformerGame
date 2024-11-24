using UnityEngine;
using static PlayerController;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float deceleration = 3f;
    public enum FacingDirection
    {
        left, right
    }
    private FacingDirection facingDirection = FacingDirection.right;

    void Start()
    {
        
    }


    void Update()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        MovementUpdate(playerInput);
        UpdateFacingDirection(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (playerInput.x != 0)
        {
            rb.AddForce(playerInput * acceleration, ForceMode2D.Force);

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        }
        else
        {
                rb.AddForce(-rb.velocity.normalized * deceleration);
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
            return false;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return FacingDirection.left;
    }
}
