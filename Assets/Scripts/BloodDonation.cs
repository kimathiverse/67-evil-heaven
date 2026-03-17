using UnityEngine;

public class BloodDonation : MonoBehaviour
{
    PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Donate()
    {
        //give karma
        //sound effect
        Debug.Log("moi");
        playerMovement.SpeedDecrease();
    }
}
