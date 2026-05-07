using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    AnimationHandeler ah;
    moveLR move;

    public GameObject spLeft;
    public GameObject spRight;
    public GameObject spUp;

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
            if (currentSp = spLeft)
            {
                Debug.Log("Left");
            }
            else if (currentSp = spRight)
            {
                Debug.Log("Right");
            }
            else
            {
                Debug.Log("Up");
            }
        }
    }
}
