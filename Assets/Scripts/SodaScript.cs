using System.Collections;
using UnityEngine;

public class SodaScript : MonoBehaviour
{
    public float drinkDuration = 1f;       // total time to lift & disappear
    public float liftHeight = 0.3f;        // how high it moves
    public float shakeAmount = 0.02f;      // small shake while moving

    private bool isDrinking = false;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!isDrinking && Input.GetMouseButtonDown(0)) // left click
        {
            StartCoroutine(DrinkRoutine());
        }
    }

    IEnumerator DrinkRoutine()
    {
        isDrinking = true;
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;

        float timer = 0f;

        while (timer < drinkDuration)
        {
            float t = timer / drinkDuration;

            // 1. Lift in a smooth arc (parabola)
            float yOffset = 4 * liftHeight * t * (1 - t); // simple parabola
            Vector3 liftPos = originalPos + new Vector3(0, yOffset, 0);

            // 2. Add small random shake
            float xShake = Random.Range(-shakeAmount, shakeAmount);
            float zShake = Random.Range(-shakeAmount, shakeAmount);
            transform.localPosition = liftPos + new Vector3(xShake, 0, zShake);

            // Optional: slight rotation for realism
            float rotZ = Mathf.Sin(t * Mathf.PI * 2) * 10f; 
            transform.localRotation = Quaternion.Euler(0, 0, rotZ);

            timer += Time.deltaTime;
            yield return null;
        }

        // Reset and disappear
        transform.localPosition = originalPos;
        transform.localRotation = originalRot;

        Destroy(gameObject);
        playerMovement.SpeedBoost();
    }
}
