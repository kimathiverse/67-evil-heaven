using UnityEngine;

public class DogSpawner : MonoBehaviour
{

    public GameObject taxManPrefab;
    public float spawnInterval = 60f;

    void Start()
    {
        InvokeRepeating("SpawnDog", 0f, spawnInterval);
    }

    void SpawnDog()
    {
        Instantiate(taxManPrefab, transform.position, Quaternion.identity);
    }
}

