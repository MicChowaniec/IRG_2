using UnityEngine;

public class RaycastCursorFollow : MonoBehaviour
{
    public Camera mainCamera; // Assign your camera in the Inspector
    public float offset = 0.5f; // Vertical offset for the object's position
    public float followSpeed = 5f; // Speed of movement towards the hit position
    public float rotationSpeed = 5f; // Speed of rotation towards the destination

    void Start()
    {
        Cursor.visible = false; // Hide the cursor

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned. Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        if (mainCamera == null) return; // Exit if no camera is assigned

        // Create a ray from the camera through the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Check if the ray hits an object in the scene
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object is the object itself or any of its children
            if (hit.collider.transform == this.transform || hit.collider.transform.IsChildOf(this.transform))
            {
                return; // If the ray hits the object or its children, do nothing
            }

            // Get the hit point
            Vector3 targetPosition = hit.point;

            // Add the offset (e.g., vertically upwards)
            targetPosition += new Vector3(0, offset, 0);

            // Gradually move the object towards the target position
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, followSpeed * Time.deltaTime);

            // Rotate the object to face the destination
            Vector3 direction = (targetPosition - this.transform.position).normalized; // Direction towards the destination
            if (direction != Vector3.zero) // Avoid rotation errors for zero direction
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
