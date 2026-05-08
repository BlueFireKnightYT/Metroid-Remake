using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float jumpForce = 7f;            // initial upward velocity
    [SerializeField] float maxJumpTime = 0.30f;       // how long holding the button continues the jump
    [SerializeField] float jumpHoldForce = 20f;       // additional upward force while holding
    [SerializeField] float jumpCutVelocity = 0f;      // vertical velocity after releasing jump (immediate drop)
    [SerializeField] Transform groundCheck;           // assign an empty at the player's feet
    [SerializeField] float groundCheckRadius = 0.08f;
    [SerializeField] LayerMask groundLayer;

    moveLR move;
    AnimationHandeler ah;
    

    public bool isJumping;
    float jumpTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<moveLR>();
        ah = GetComponent<AnimationHandeler>();
    }

    // New Input System callback
    public void Jump(InputAction.CallbackContext context)
    {
        if (move.isRolling)
        {
            move.EndRoll();
            ah.EndRollAnim();
        }
        else
        {
            if (context.started)
            {

                if (IsGrounded())
                {
                    isJumping = true;
                    jumpTimeCounter = maxJumpTime;

                    // set initial upward velocity (use new linearVelocity)
                    var v = rb.linearVelocity;
                    v.y = jumpForce;
                    rb.linearVelocity = v;
                }
            }
            else if (context.performed)
            {
                // while button is held, apply a short upward acceleration until maxJumpTime runs out
                if (isJumping && jumpTimeCounter > 0f)
                {
                    // apply small upward force per frame for a variable-height jump
                    rb.AddForce(Vector2.up * jumpHoldForce * Time.deltaTime, ForceMode2D.Force);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
            else if (context.canceled)
            {
                // Immediate drop: if currently moving upward, cut the upward velocity
                var v = rb.linearVelocity;
                if (v.y > jumpCutVelocity)
                {
                    v.y = jumpCutVelocity;
                    rb.linearVelocity = v;
                }

                // stop extending the jump
                isJumping = false;
                jumpTimeCounter = 0f;
            }
        }
    }

    void Update()
    {
        // Stop applying hold-forces when time runs out
        if (isJumping && jumpTimeCounter <= 0f)
            isJumping = false;
    }

    public bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
