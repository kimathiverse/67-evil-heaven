using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KarmaSlider : MonoBehaviour {
    /* public KarmaController Karma;
    [SerializeField] public Slider slider; */

    public int maximum;
    public int current;
    public Image mask;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        GetCurrentFill();
    }

    void GetCurrentFill() {
        float fillAmount = (float)current/(float)maximum;
        mask.fillAmount = fillAmount;
    }

    /* float CalculateSliderValue() {
        return (Karma);
    } */
}