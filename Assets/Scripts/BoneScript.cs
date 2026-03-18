using UnityEngine;

public class BoneScript : MonoBehaviour
{

    public Camera cam; // assign in Inspector
    public float rayDistance = 10f;
    private DogMovement dogMovement;
    private PuppyMovement puppyMovement;

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
         Ray ray = new Ray(cam.transform.position, cam.transform.forward);
         RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
                if (hit.collider.CompareTag("Dog"))
                {
                    Debug.Log("moi");
                    dogMovement = hit.collider.gameObject.GetComponent<DogMovement>();
                    dogMovement.breeding = true;
                    Destroy(gameObject);
                }
                if (hit.collider.CompareTag("Puppy"))
                {
                    puppyMovement = hit.collider.gameObject.GetComponent<PuppyMovement>();
                    puppyMovement.breeding = true;
                    Destroy(gameObject);
                }

        }
        }
  
    }
}
