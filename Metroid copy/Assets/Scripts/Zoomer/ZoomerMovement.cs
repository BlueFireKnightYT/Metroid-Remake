using UnityEngine;

public class ZoomerMovement : MonoBehaviour
{
    public bool inverted;
    public float moveSpeed = 2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength = 0.5f;

    void FixedUpdate()
    {
        float direction = inverted ? -1f : 1f;

        transform.Translate(Vector3.right * moveSpeed * direction * Time.fixedDeltaTime, Space.Self);

        Vector2 forwardDir = (Vector2)transform.right * direction;
        Vector2 downDir = -transform.up;

        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, forwardDir, rayLength, groundLayer);

        Vector2 edgeCheckOrigin = (Vector2)transform.position + (forwardDir * 0.2f);
        RaycastHit2D floorHit = Physics2D.Raycast(edgeCheckOrigin, downDir, rayLength, groundLayer);

        Debug.DrawRay(transform.position, forwardDir * rayLength, Color.green);
        Debug.DrawRay(edgeCheckOrigin, downDir * rayLength, Color.red);

        if (wallHit.collider != null)
        {
            transform.up = wallHit.normal;
            transform.position = wallHit.point + (wallHit.normal * 0.25f);
            transform.Rotate(0, 0, 90f * direction);
            return;
        }

        if (floorHit.collider == null)
        {
            transform.Translate(Vector3.right * 0.25f * direction, Space.Self);
            transform.Rotate(0, 0, -90f * direction);

            RaycastHit2D realignHit = Physics2D.Raycast(transform.position, -transform.up, rayLength * 2f, groundLayer);
            if (realignHit.collider != null)
            {
                transform.up = realignHit.normal;
                transform.position = realignHit.point + (realignHit.normal * 0.25f);
            }
        }
    }
}