using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandeler : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    moveLR move;

    public bool isLookingUp;
    public bool isSpriteFlipped;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        move = GetComponent<moveLR>();
    }
    private void Update()
    {
        if (rb.linearVelocityX != 0)
        {
            sr.flipX = isSpriteFlipped = (rb.linearVelocityX < 0) ? true : false;
        }
            

        bool movingBool = (rb.linearVelocityX != 0) ? true : false;
        anim.SetBool("Moving", movingBool);

        
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && move.rollingUnlocked && rb.linearVelocityX == 0)
        {
            anim.SetBool("Rolling", true);
        }
    }
    void EndRollAnim()
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
