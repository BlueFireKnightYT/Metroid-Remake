using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    float startHP = 30;
    float currentHP;
    public TextMeshProUGUI hpText;
    public float knockbackForce;
    public float stunTime = 0.18f;
    Rigidbody2D rb;
    moveLR move;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<moveLR>();
        currentHP = startHP;
        hpText.text = currentHP.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            currentHP -= 8;
            hpText.text = currentHP.ToString();

            if (currentHP <= 0)
                SceneManager.LoadScene("Level1");

            Vector2 dir = (transform.position - collision.transform.position).normalized;
            rb.linearVelocity = dir * knockbackForce;
            if (move != null) StartCoroutine(StunCoroutine());
        }
    }

    private IEnumerator StunCoroutine()
    {
        if (move != null) move.isStunned = true;
        yield return new WaitForSeconds(stunTime);
        if (move != null) move.isStunned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HP"))
        {
            currentHP += 5;
            hpText.text = currentHP.ToString();
            Destroy(collision.gameObject);
        }
    }
}
