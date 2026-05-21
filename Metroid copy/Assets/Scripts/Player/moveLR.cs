using UnityEngine;
using UnityEngine.InputSystem;

public class moveLR : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 4f;
    Vector2 moveInput;

    BoxCollider2D standingColl;
    CircleCollider2D rollingColl;

    public bool rollingUnlocked = false;
    public bool isRolling = false;
    public bool isStunned = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standingColl = GetComponent<BoxCollider2D>();
        rollingColl = GetComponent<CircleCollider2D>();

        rollingColl.enabled = false;
    }

    private void FixedUpdate()
    {
        float targetX = isStunned ? rb.linearVelocityX : moveInput.x * moveSpeed;
        rb.linearVelocity = new Vector2(targetX, rb.linearVelocityY);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (context.performed && rollingUnlocked && rb.linearVelocityX == 0)
        {
            rollingColl.enabled = true;
            standingColl.enabled = false;
            isRolling = true;
        }
    }

    public void EndRoll()
    {
        rollingColl.enabled = false;
        standingColl.enabled = true;
        isRolling = false;
    }
}
