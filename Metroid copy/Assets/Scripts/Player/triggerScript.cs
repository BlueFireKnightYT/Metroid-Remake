using UnityEngine;

public class triggerScript : MonoBehaviour
{
    SkreeScript ss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        { 
            ss = GetComponentInParent<SkreeScript>();
            ss.isAttacking = true;
            this.gameObject.SetActive(false);
        }
    }
}
