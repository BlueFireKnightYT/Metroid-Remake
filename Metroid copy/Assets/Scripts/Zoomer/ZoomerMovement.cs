using UnityEngine;

public class ZoomerMovement : MonoBehaviour
{
    [SerializeField] Transform raycastPos;
    [SerializeField] float moveSpeed;
    [SerializeField] float rayLen;

    private int directionIndex = 0;

    bool up, down, left, right = true;
    Vector3 moveVector;

    void FixedUpdate()
    {
        RaycastHit2D downCast = Physics2D.Raycast(raycastPos.position, -transform.up, rayLen);
        Debug.DrawRay(raycastPos.position, -transform.up * rayLen, Color.red);

        if (downCast.collider == null)
        {
            RotateAndCycle();
        }

        if (right) moveVector = Vector3.right;
        else if (up) moveVector = Vector3.up;
        else if (left) moveVector = Vector3.left;
        else if (down) moveVector = Vector3.down;

        transform.position += moveVector * moveSpeed * Time.fixedDeltaTime;
    }

    void RotateAndCycle()
    {
        transform.Rotate(0, 0, 90f);

        directionIndex = (directionIndex + 1) % 4;

        right = (directionIndex == 0);
        up = (directionIndex == 1);
        left = (directionIndex == 2);
        down = (directionIndex == 3);
    }
}
