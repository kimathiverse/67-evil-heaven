using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject shopUI;
    private PlayerMovement playerMovement;
    private PlayerMoney playerMoney;
    private PlayerCam playerCam;

    public GameObject boneObject;
    public GameObject boneUI;
    public Transform inventory;
    public Transform spawnPoint;
    public Transform itemRotation;
    public GameObject EnergyObject;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCam>();
        playerMoney = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoney>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenShop()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        shopUI.SetActive(true);
        playerMovement.canMove = false;
        playerCam.canMoveCamera = false;
    }
    public void CloseShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        shopUI.SetActive(false);
        playerMovement.canMove = true;
        playerCam.canMoveCamera = true;
    }
    public void BuyBone()
    {
       if(playerMoney.money >= 20)
        {
            GameObject newBone = Instantiate(boneObject, spawnPoint.position, itemRotation.rotation, inventory);
            newBone.SetActive(false); 
            playerMoney.money -= 20;
            boneUI.SetActive(false);
            playerMoney.UpdateMoney();
        }

    }
    public void BuyEnergy()
    {
        if(playerMoney.money >= 10)
        {
            GameObject newEnergy = Instantiate(EnergyObject, spawnPoint.position, itemRotation.rotation, inventory);
            newEnergy.SetActive(false);
            playerMoney.money -= 10;
            playerMoney.UpdateMoney();
        }
    }
}
