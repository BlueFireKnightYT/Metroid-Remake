using System.Collections;
using UnityEngine;

public class RollUnlockScript : MonoBehaviour
{
    GameObject player;
    moveLR move;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        move = player.GetComponent<moveLR>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            move.rollingUnlocked = true;
            StartCoroutine(FreezePause());
        }
    }

    IEnumerator FreezePause()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        Destroy(this.gameObject);
    }

}
