using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public float bulletSpeedX;
    public float bulletSpeedY;

    private void FixedUpdate()
    {
        transform.position += new Vector3(bulletSpeedX, bulletSpeedY, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
