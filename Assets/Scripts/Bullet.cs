using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("MOI");
        Destroy(gameObject);
    }
}
