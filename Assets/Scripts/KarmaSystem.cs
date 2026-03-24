using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarmaSystem : MonoBehaviour
{
    public float karma;
    public Slider karmaSlider;
    public bool gameEnded;
    public GameObject hellObject;
    public GameObject heavenObject;
    public GameObject moneyObject;
    public GameObject hpObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        karma = 100f;
        karmaSlider.value = 0.5f;
        karmaSlider.maxValue = 1f;
        gameEnded = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (karma >= 200 && !gameEnded)
        {
            GetToHeaven();
        }
        else if (karma <= 0 && !gameEnded)
        {
            GetToHell();
        }
    }
    public void GetKarma(float karmaGotten)
    {
        karma += karmaGotten;
        karmaSlider.value = karma / 200;
    }
    public void LoseKarma(float karmaLost)
    {
        karma -= karmaLost;
        karmaSlider.value = karma / 200;
    }
    public void GetToHeaven()
    {
        Debug.Log("heaven");
        heavenObject.SetActive(true);
        Time.timeScale = 0;
        gameEnded = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        moneyObject.SetActive(false);
        hpObject.SetActive(true);


    }
    public void GetToHell()
    {
        Debug.Log("Hell");
        hellObject.SetActive(true);
        Time.timeScale = 0;
        gameEnded = true;
                Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        moneyObject.SetActive(false);
        hpObject.SetActive(false);
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

}
