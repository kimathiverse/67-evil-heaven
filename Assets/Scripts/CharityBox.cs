using UnityEngine;

public class CharityBox : MonoBehaviour
{
    private PlayerMoney playerMoney;
    private KarmaSystem karmaSystem;
    public float donationAmount;
    public float karmaFromAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
        karmaSystem = GameObject.FindGameObjectWithTag("Karma").GetComponent<KarmaSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Donate()
    {
        if(playerMoney.money >= donationAmount)
        {
            playerMoney.money -= donationAmount;
            playerMoney.UpdateMoney();
            karmaSystem.GetKarma(karmaFromAmount);
            // play sound
        }
        //else play error sound
        
    }
}
