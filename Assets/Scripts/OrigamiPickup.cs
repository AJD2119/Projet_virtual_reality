using UnityEngine;
using TMPro;
using System.Collections.Generic;
using cakeslice; // For Outline

public class OrigamiPickupSystem : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRange = 3f;
    public KeyCode pickupKey = KeyCode.E;
    public LayerMask easterEggLayer;

    [Header("UI")]
    public TextMeshProUGUI pickupPrompt;
    public TextMeshProUGUI counterText;

    [Header("Inventory")]
    public List<string> collectedOrigamis = new List<string>();
    public int totalOrigamis = 12;

    [Header("Audio")]
    public AudioSource audioSource;       // Drag your AudioSource here
    public AudioClip pickupSound;         // Drag your paper crumble sound here

    private Camera cam;
    private Outline lastHighlighted;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            Debug.LogError("OrigamiPickupSystem must be attached to the Main Camera!");

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        if (counterText != null)
            counterText.text = $"Origamis: 0 / {totalOrigamis}";
    }

    void Update()
    {
        HandleHighlighting();
        HandlePickup();

        Debug.DrawRay(cam.transform.position, cam.transform.forward * pickupRange, Color.green);
    }

    void HandleHighlighting()
    {
        if (lastHighlighted != null)
        {
            lastHighlighted.enabled = false;
            lastHighlighted = null;
        }

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, easterEggLayer))
        {
            if (hit.collider.CompareTag("Origami"))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                    lastHighlighted = outline;
                }

                if (pickupPrompt != null)
                    pickupPrompt.gameObject.SetActive(true);
            }
        }
    }

    void HandlePickup()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange, easterEggLayer))
            {
                if (hit.collider.CompareTag("Origami"))
                {
                    Debug.Log("Picked up: " + hit.collider.name);
                    collectedOrigamis.Add(hit.collider.name);

                    if (counterText != null)
                        counterText.text = $"Origamis: {collectedOrigamis.Count} / {totalOrigamis}";

                    Outline outline = hit.collider.GetComponent<Outline>();
                    if (outline != null)
                        outline.enabled = false;

                    // 🔊 Play crumble/paper sound
                    if (audioSource != null && pickupSound != null)
                        audioSource.PlayOneShot(pickupSound);

                    Destroy(hit.collider.gameObject);

                    if (pickupPrompt != null)
                        pickupPrompt.gameObject.SetActive(false);
                }
            }
        }
    }
}
