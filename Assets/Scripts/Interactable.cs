using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public Outline outline;
    public string message;

    public UnityEvent onInteraction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        Debug.Log(outline);
        DisableOutline();
    }
    public void Interact()
    {
        onInteraction.Invoke();
    }
    public void DisableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }


}
