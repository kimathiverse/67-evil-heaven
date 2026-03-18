using UnityEngine;

public class BloodDonation : MonoBehaviour
{
    PlayerMovement playerMovement;
    KarmaSystem karmaSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        karmaSystem = GameObject.FindGameObjectWithTag("Karma").GetComponent<KarmaSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Donate()
    {
        karmaSystem.GetKarma(10);
        //sound effect
        playerMovement.SpeedDecrease();
    }
}
