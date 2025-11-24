using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Debug.Log("Ray hit: " + hit.collider.name);  // <-- AJOUT TEMPORAIRE
            FoxPetTrigger fox = hit.collider.GetComponent<FoxPetTrigger>();

            if (fox != null)
            {
                Debug.Log("Fox detected, sending Pet()!"); // <-- AJOUT TEMPORAIRE
                fox.Pet();
            }
        }
    }
}
