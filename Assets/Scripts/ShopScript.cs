using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject shopUI;
    private PlayerMovement playerMovement;
    private PlayerCam playerCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCam>();
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
}
