using UnityEngine;

public class TaxManSpawner : MonoBehaviour
{
    
    public GameObject taxManPrefab;
    public float spawnInterval = 10f;

    void Start()
    {
        InvokeRepeating("SpawnTaxMan", 0f, spawnInterval);
    }

    void SpawnTaxMan()
    {
        Instantiate(taxManPrefab, transform.position, Quaternion.identity);
    }

}
