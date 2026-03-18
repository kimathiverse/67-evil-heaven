using UnityEngine;
using UnityEngine.UI;

public class KarmaSystem : MonoBehaviour
{
    public float karma;
    public Slider karmaSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        karma = 100f;
        karmaSlider.value = 0.5f;
        karmaSlider.maxValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (karma >= 200)
        {
            GetToHeaven();
        }
        else if (karma <= 0)
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
    }
    public void GetToHell()
    {
        Debug.Log("Hell");
    }

}
