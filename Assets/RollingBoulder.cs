using UnityEngine;
using TMPro;

public class RollingBoulder : MonoBehaviour
{
    [Header("UI References")]
    public GameObject InfoPanel;           // Info panel
    public TMP_Text BoulderInfoText;       // Text inside panel
    public GameObject NextButton;          // Next button object
    public GameObject ExitButton;          // Exit button object

    [Header("Floating Prompt")]
    public TMP_Text interactionPrompt;     // Floating world-space "Press F" text
    public Vector3 promptOffset = new Vector3(0, 3f, 0); // How high above boulder

    [Header("Interaction Settings")]
    public Transform player;
    public float interactionDistance = 6f; // How close player must be to interact

    private int currentPage = 0;

    void Start()
    {
        // Hide panel initially
        InfoPanel.SetActive(false);

        // Show Next button at start
        if (NextButton != null)
            NextButton.SetActive(true);
    }

    void Update()
    {
        UpdatePrompt();
        CheckInteraction();
    }

    // -----------------------------
    // Floating prompt logic
    // -----------------------------
    void UpdatePrompt()
    {
        if (interactionPrompt == null || player == null) return;

        // Position above boulder
        interactionPrompt.transform.position = transform.position + promptOffset;

        // Make it face the camera
        if (Camera.main != null)
        {
            Vector3 direction = interactionPrompt.transform.position - Camera.main.transform.position;
            interactionPrompt.transform.rotation = Quaternion.LookRotation(direction);
        }

        // Show/hide based on distance
        float distance = Vector3.Distance(player.position, transform.position);
        interactionPrompt.gameObject.SetActive(distance <= interactionDistance);
    }

    // -----------------------------
    // Player interaction
    // -----------------------------
    void CheckInteraction()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F))
        {
            OpenPanel();
        }
    }

    void OpenPanel()
    {
        currentPage = 0;
        InfoPanel.SetActive(true);
        if (NextButton != null)
            NextButton.SetActive(true);

        ShowPage();

        // Unlocks cursor so buttons can be clicked
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void NextPage()
    {
        currentPage++;
        ShowPage();

        // Hides Next button after second page
        if (currentPage >= 1 && NextButton != null)
            NextButton.SetActive(false);
    }

    public void ExitPanel()
    {
        InfoPanel.SetActive(false);

        // Locks cursor back for gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ShowPage()
    {
        if (BoulderInfoText == null) return;

        if (currentPage == 0)
        {
            BoulderInfoText.text = PAGE_1;
        }
        else if (currentPage == 1)
        {
            BoulderInfoText.text = PAGE_2;
        }
    }

    // -----------------------------
    // Page content
    // -----------------------------
    const string PAGE_1 =
@"Gravity is the force that pulls objects toward the Earth.

As the boulder rolls down the mountain, gravity accelerates it downward.
The steeper the slope, the stronger the component of gravity acting along
the surface, causing the boulder to speed up.

Friction and air resistance oppose this motion, slowing the boulder down
over time. When these forces balance gravity, the boulder eventually
comes to rest.";

    const string PAGE_2 =
@"Isaac Newton was one of the first scientists to describe gravity mathematically.

He proposed that every object with mass attracts every other object with mass.
This idea became known as Newton’s Law of Universal Gravitation.

Newton also described motion using three laws, explaining how forces like
gravity cause objects to accelerate. These discoveries laid the foundation
for classical physics and our modern understanding of motion.";
}