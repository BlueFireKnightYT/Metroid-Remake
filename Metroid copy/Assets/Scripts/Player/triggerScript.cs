using UnityEngine;

public class triggerScript : MonoBehaviour
{
    SkreeScript ss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ss = GetComponentInParent<SkreeScript>();
        ss.isAttacking = true;
    }
}
