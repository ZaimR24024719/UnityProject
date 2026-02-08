using UnityEngine;
using TMPro;

public class RollingBoulder : MonoBehaviour
{
    [Header("References")]
    public GameObject BoulderInfoTextPanel; // Panel to show/hide
    public TMP_Text BoulderInfoText;        // Text inside the panel
    public TMP_Text interactionPrompt;      // World-space floating prompt (Canvas NOT child of boulder)
    public Transform player;                // Player transform

    [Header("Settings")]
    public float interactionDistance = 6f; // Distance for player to see prompt & interact
    public Vector3 promptOffset = new Vector3(0, 3f, 0); // Offset above boulder

    int page = 0;

    void Start()
    {
        if (BoulderInfoTextPanel != null)
            BoulderInfoTextPanel.SetActive(false);

        if (interactionPrompt != null)
            interactionPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdatePrompt();
        CheckInteraction();
    }

    // Make the prompt follow the boulder but not rotate with it
    void UpdatePrompt()
    {
        if (interactionPrompt == null || player == null) return;

        // Follow the boulder position
        interactionPrompt.transform.position = transform.position + promptOffset;

        // Always face the camera
        if (Camera.main != null)
        {
            Vector3 direction = interactionPrompt.transform.position - Camera.main.transform.position;
            interactionPrompt.transform.rotation = Quaternion.LookRotation(direction);
        }

        // Show/hide prompt based on distance to player
        float distance = Vector3.Distance(player.position, transform.position);
        interactionPrompt.gameObject.SetActive(distance <= interactionDistance);
    }

    // Check if player presses F while close to boulder
    void CheckInteraction()
    {
        if (interactionPrompt == null || player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            if (BoulderInfoTextPanel != null)
                BoulderInfoTextPanel.SetActive(true);

            page = 0;
            ShowPage();
        }
    }

    // Called by Next button
    public void NextPage()
    {
        page++;
        ShowPage();
    }

    // Called by Exit button
    public void ExitPanel()
    {
        if (BoulderInfoTextPanel != null)
            BoulderInfoTextPanel.SetActive(false);
    }

    // Update text content manually in Unity
    void ShowPage()
    {
        // Page content will be set in Unity via BoulderInfoText.text
    }
}