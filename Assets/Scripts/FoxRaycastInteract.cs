using UnityEngine;

public class FoxRaycastInteract : MonoBehaviour
{
    public FoxPetTrigger fox; // drag & drop ton renard dans l'inspecteur

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T"); 
            if (fox != null)
            {
                fox.Pet();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R"); 
            if (fox != null)
            {
                fox.Roulade();
            }
        }
    }
}
