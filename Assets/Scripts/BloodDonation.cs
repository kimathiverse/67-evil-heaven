using UnityEngine;

public class BloodDonation : MonoBehaviour
{
    PlayerMovement playerMovement;
    KarmaSystem karmaSystem;
    public float karmaFrom;
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
        if(playerMovement.decreased == false)
        {
        karmaSystem.GetKarma(karmaFrom);
        //sound effect
        playerMovement.SpeedDecrease();
        }

    }
}
