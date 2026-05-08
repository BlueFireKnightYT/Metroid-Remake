using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandeler : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    moveLR move;
    PlayerJump pj;

    public bool isLookingUp;
    public bool isSpriteFlipped;
    public bool isMoving;
    bool hasChecked = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        pj = GetComponent<PlayerJump>();
        anim = GetComponent<Animator>();
        move = GetComponent<moveLR>();
    }
    private void Update()
    {
        isMoving = (rb.linearVelocityX != 0) ? true : false;

        if (rb.linearVelocityX != 0)
        {
            sr.flipX = isSpriteFlipped = (rb.linearVelocityX < 0) ? true : false;
        }
            

        bool movingBool = (rb.linearVelocityX != 0) ? true : false;
        anim.SetBool("Moving", movingBool);

        if (!pj.IsGrounded())
        {
            if (!hasChecked)
            {
                if (isMoving)
                {
                    anim.SetBool("Jumping", true);
                    anim.SetBool("Flipping", true);
                }
                else
                {
                    anim.SetBool("Jumping", true);
                }
                hasChecked = true;
            }
        }
        if (pj.IsGrounded())
        {
            hasChecked = false;
            anim.SetBool("Jumping", false);
            anim.SetBool("Flipping", false);
        }
            
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && move.rollingUnlocked && rb.linearVelocityX == 0)
        {
            anim.SetBool("Rolling", true);
        }
    }
    public void EndRollAnim()
    {
        anim.SetBool("Rolling", false);
    }

    public void LookUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (move.isRolling)
            {
                move.EndRoll();
                EndRollAnim();
            }
            else 
            { 
                anim.SetBool("LookingUp", true);
                isLookingUp = true;
            }
        }
        if (context.canceled)
        {
            anim.SetBool("LookingUp", false);
            isLookingUp = false;
        }
    }

}
