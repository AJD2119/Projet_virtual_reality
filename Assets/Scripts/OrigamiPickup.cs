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
    public TextMeshProUGUI counterText; // Displays "Found X / 12"

    [Header("Inventory")]
    public List<string> collectedOrigamis = new List<string>();
    public int totalOrigamis = 12; // Total origamis in the level

    private Camera cam;
    private Outline lastHighlighted;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
            Debug.LogError("OrigamiPickupSystem must be attached to the Main Camera!");

        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(false);

        // Initialize counter
        if (counterText != null)
            counterText.text = $"Origamis: 0 / {totalOrigamis}";
    }

    void Update()
    {
        HandleHighlighting();
        HandlePickup();

        // Visualize ray in Scene view
        Debug.DrawRay(cam.transform.position, cam.transform.forward * pickupRange, Color.green);
    }

    void HandleHighlighting()
    {
        // Disable previous outline & hide prompt
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
            // Debug: show what the raycast hits
            Debug.Log("Ray hit: " + hit.collider.name +
                      " | Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer) +
                      " | Tag: " + hit.collider.tag);

            if (hit.collider.CompareTag("Origami"))
            {
                // Enable outline
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                    lastHighlighted = outline;
                }

                // Show UI prompt
                if (pickupPrompt != null)
                    pickupPrompt.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Ray hit nothing");
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
                // Debug: show what is being picked up
                Debug.Log("Pickup Ray hit: " + hit.collider.name +
                          " | Layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer) +
                          " | Tag: " + hit.collider.tag);

                if (hit.collider.CompareTag("Origami"))
                {
                    Debug.Log("Picked up: " + hit.collider.name);
                    collectedOrigamis.Add(hit.collider.name);

                    // Update on-screen counter
                    if (counterText != null)
                        counterText.text = $"Origamis: {collectedOrigamis.Count} / {totalOrigamis}";

                    // Disable outline
                    Outline outline = hit.collider.GetComponent<Outline>();
                    if (outline != null)
                        outline.enabled = false;

                    Destroy(hit.collider.gameObject);

                    // Hide pickup prompt
                    if (pickupPrompt != null)
                        pickupPrompt.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("Pickup Ray hit nothing");
            }
        }
    }
}
