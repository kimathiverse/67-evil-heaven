using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
public static HUDController instance;

    void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (E)";
        interactionText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
