using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    AnimationHandeler ah;
    moveLR move;

    public GameObject spLeft;
    public GameObject spRight;
    public GameObject spUp;

    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject bulletPrefab;
    GameObject thisBullet;

    GameObject currentSp;

    private void Start()
    {
        ah = GetComponent<AnimationHandeler>();
        move = GetComponent<moveLR>();
    }

    private void Update()
    {
        if (ah.isLookingUp)
        {
            currentSp = spUp;
        }
        else if(ah.isSpriteFlipped)
        {
            currentSp = spLeft;
        }
        else
        {
            currentSp = spRight;
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        { 
            if (currentSp == spLeft)
            {
                Debug.Log("Left");
                thisBullet = Instantiate(bulletPrefab, currentSp.transform.position, Quaternion.identity);
                bulletScript bs = thisBullet.GetComponent<bulletScript>();
                bs.bulletSpeedX = -bulletSpeed;
            }
            else if (currentSp == spRight)
            {
                Debug.Log("Right");
                thisBullet = Instantiate(bulletPrefab, currentSp.transform.position, Quaternion.identity);
                bulletScript bs = thisBullet.GetComponent<bulletScript>();
                bs.bulletSpeedX = bulletSpeed;
            }
            else
            {
                Debug.Log("Up");
                thisBullet = Instantiate(bulletPrefab, currentSp.transform.position, Quaternion.identity);
                bulletScript bs = thisBullet.GetComponent<bulletScript>();

                bs.bulletSpeedY = bulletSpeed;
            }

            Destroy(thisBullet, .2f);
            
        }
    }
}
