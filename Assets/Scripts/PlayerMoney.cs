using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public float money;
    public TMP_Text moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = "Money: " + money + "€";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetMoney(float amount)
    {
        money += amount;
        moneyText.text = "Money: " + money + "€";
    }
}
