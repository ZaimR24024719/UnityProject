using UnityEngine;

public class SimpleFPSController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 150f;
    public float gravity = -9.81f;

    public Transform cameraTransform;

    float xRotation = 0f;
    Vector3 velocity;
    CharacterController controller;

    // 🔹 NEW
    public bool controlsEnabled = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        LockCursor();
    }

    void Update()
    {
        if (!controlsEnabled)
            return; // ⛔ stop camera + movement completely

        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Movement
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += transform.forward;
        if (Input.GetKey(KeyCode.S)) move -= transform.forward;
        if (Input.GetKey(KeyCode.A)) move -= transform.right;
        if (Input.GetKey(KeyCode.D)) move += transform.right;

        controller.Move(move.normalized * moveSpeed * Time.deltaTime);

        // Gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // 🔹 Cursor helpers
    public void UnlockCursor()
    {
        controlsEnabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockCursor()
    {
        controlsEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}