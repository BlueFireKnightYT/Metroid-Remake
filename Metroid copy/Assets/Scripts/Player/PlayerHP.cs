using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    float startHP = 30;
    float currentHP;
    public TextMeshProUGUI hpText;


    private void Start()
    {
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
        }
    }


}
