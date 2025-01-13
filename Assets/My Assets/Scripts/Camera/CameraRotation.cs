using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("Camera Rotation Settings")]
    [SerializeField]
    private float rotationSpeed = 5f; // Speed of the camera rotation

    private bool isRotating = false;

    void Update()
    {
        // Check if the middle mouse button (scroll wheel) is pressed
        if (Input.GetMouseButtonDown(2))
        {
            isRotating = true; // Start rotating
        }
        if (Input.GetMouseButtonUp(2))
        {
            isRotating = false; // Stop rotating
        }

        // If rotating, adjust the camera's rotation
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X"); // Get horizontal mouse movement

            // Rotate the camera around its Y-axis (left and right)
            transform.Rotate(Vector3.up, mouseX * rotationSpeed, Space.World);
        }
    }
}
