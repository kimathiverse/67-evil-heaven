using UnityEngine;

public class CharityBox : MonoBehaviour
{
    private PlayerMoney playerMoney;
    public float donationAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
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
            //give karma
            // play sound
        }
        //else play error sound
        
    }
}
