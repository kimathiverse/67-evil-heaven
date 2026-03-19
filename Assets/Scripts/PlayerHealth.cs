using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Death();
        }
    }
    public void Death()
    {
        Debug.Log("kuolit");
    }
    public void TakeDamage(float takenDamage)
    {
        health -= takenDamage;
        Debug.Log(health);
    }
}
