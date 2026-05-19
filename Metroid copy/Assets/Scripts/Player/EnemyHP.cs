using System.Collections;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] float bulletDMG = 10;

    [SerializeField] GameObject hpPickup;
    [SerializeField] GameObject deathExplosion;

    bool canBeHit = true;
    float currentHP;
    SpriteRenderer sr;
    ZoomerMovement move;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        move = GetComponent<ZoomerMovement>();
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && canBeHit)
        {
            currentHP -= bulletDMG;

            if (currentHP <= 0)
            {
                float HPSpawnChance = Random.Range(0, 6);
                if (HPSpawnChance == 0)
                { 
                    //Instantiate(hpPickup, transform.position, Quaternion.identity);
                    Debug.Log("HP");
                }

                Instantiate(deathExplosion, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                StartCoroutine(hurtEvent());
            }
        }
    }

    IEnumerator hurtEvent()
    {
        sr.color = Color.red;
        float ogMoveSpeed = move.moveSpeed;
        move.moveSpeed = 0f;
        canBeHit = false;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
        move.moveSpeed = ogMoveSpeed;
        canBeHit = true;
    }
}
