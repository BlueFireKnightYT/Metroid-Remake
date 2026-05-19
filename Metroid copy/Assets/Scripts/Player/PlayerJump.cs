using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float jumpForce = 7f;
    [SerializeField] float maxJumpTime = 0.30f;
    [SerializeField] float jumpHoldForce = 20f;       
    [SerializeField] float jumpCutVelocity = 0f;      
    [SerializeField] Transform groundCheck;           
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

                    var v = rb.linearVelocity;
                    v.y = jumpForce;
                    rb.linearVelocity = v;
                }
            }
            else if (context.performed)
            {
                if (isJumping && jumpTimeCounter > 0f)
                {
                    rb.AddForce(Vector2.up * jumpHoldForce * Time.deltaTime, ForceMode2D.Force);
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
            else if (context.canceled)
            {
                var v = rb.linearVelocity;
                if (v.y > jumpCutVelocity)
                {
                    v.y = jumpCutVelocity;
                    rb.linearVelocity = v;
                }

                isJumping = false;
                jumpTimeCounter = 0f;
            }
        }
    }

    void Update()
    {
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
