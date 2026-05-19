using UnityEngine;

public class SkreeScript : MonoBehaviour
{
    public Transform playerPos;
    public bool isAttacking;
    private void FixedUpdate()
    {
        if (isAttacking)
        {
            float moveDir = (playerPos.transform.position.x > transform.position.x) ? .07f : -.07f;
            transform.position += new Vector3(moveDir, -.1f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isAttacking = false;
        Destroy(this.gameObject, 2);
    }
}
