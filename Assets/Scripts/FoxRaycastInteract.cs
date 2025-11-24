using UnityEngine;

public class FoxRaycastInteract : MonoBehaviour
{
    public FoxPetTrigger fox;        // renard
    public GameObject hintUI;        // texte "T pour caresser"
    public float interactDistance = 3f;

    void Start()
    {
        if (hintUI != null)
            hintUI.SetActive(false);
    }

    void Update()
    {
        HandleUI();
        HandleInput();
    }

    void HandleUI()
    {
        float distance = Vector3.Distance(transform.position, fox.transform.position);

        if (distance <= interactDistance)
        {
            if (!hintUI.activeSelf)
                hintUI.SetActive(true);
        }
        else
        {
            if (hintUI.activeSelf)
                hintUI.SetActive(false);
        }
    }

    void HandleInput()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T : Assis");
            fox.Pet();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R : Jouer");
            fox.Roulade();
        }
    }
}
