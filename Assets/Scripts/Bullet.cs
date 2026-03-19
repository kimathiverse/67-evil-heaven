using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TaxMan"))
        {
            TaxManAI taxManAI;
            taxManAI = collision.gameObject.GetComponent<TaxManAI>();

            taxManAI.TakeDamage(30);
            Debug.Log("taxman hit");
        }
        Destroy(gameObject);
    }
}
