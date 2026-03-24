using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public TMP_Text hpText;
    bool dead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
        hpText.text = health.ToString();
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !dead)
        {
            Death();
            dead = true;
        }
    }
    public void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }
    public void TakeDamage(float takenDamage)
    {
        health -= takenDamage;
        Debug.Log(health);
        hpText.text = health.ToString();

    }
}
