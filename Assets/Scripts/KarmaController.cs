using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KarmaController : MonoBehaviour {
    [Range(665.0f, 778.0f)]
    public float Karma = 710.0f; // strat off karma
    public float KarmaAddition = 10.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (Keyboard.current.numpad7Key.wasPressedThisFrame) {
            Karma = Karma + KarmaAddition;
            Debug.Log($"You now have {Karma} Karma");
        }

        if (Keyboard.current.numpad6Key.wasPressedThisFrame) {
            Karma = Karma - KarmaAddition;
            Debug.Log($"You now have {Karma} Karma!");
        }

        if (Karma < 666) {
            GoToHell(); // go to hell
        }

        if (Karma > 777) {
            GoToHeaven(); // go to heaven
        }
    }

    public void GoToHeaven() {
        if (Karma > 777) {
            SceneManager.LoadScene(1); // temporary scene
        }
    }

    public void GoToHell() {
        if(Karma < 666) {
            SceneManager.LoadScene(2); // temporary scene teleporter to see if it works
        }
    }
}
