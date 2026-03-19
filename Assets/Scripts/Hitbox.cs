
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    PlayerHealth playerHealth;
    Rigidbody playerRb;
    public float kbForce;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBox"))
        {
            Debug.Log("moi");
            playerHealth = other.GetComponentInParent<PlayerHealth>();
            playerHealth.TakeDamage(20f);
            Transform player;
            Transform taxMan;

            playerRb = other.GetComponentInParent<Rigidbody>();
            player = other.GetComponentInParent<Transform>();
            taxMan = gameObject.GetComponentInParent<Transform>();
            Vector3 kbDirection = (player.position - taxMan.position).normalized;

            playerRb.AddForce(new Vector3(kbDirection.x * 10, kbDirection.y + 1, kbDirection.z * 10) * kbForce, ForceMode.Impulse);



        }
    }
}
