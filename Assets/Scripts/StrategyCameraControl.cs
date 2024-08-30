using UnityEngine;

public class StrategyCameraControl : MonoBehaviour
{
    public float panSpeed = 20f; // Speed of panning
    public float scrollSpeed = 10f; // Base speed of scrolling for moving camera position
    public float rotateSpeed = 100f; // Speed of rotation
    public float fixedHeight = 10f; // Fixed height for the camera when centering
    public float zoomSmoothness = 5f; // Smoothness factor for zooming
    public float spaceMoveHeight = 10f; // Height to move up when space is pressed
    public float nonLinearFactor = 1.5f; // Factor for non-linear zoom scaling
    public float minY = 5f; // Minimum allowed height for the camera
    public float maxY = 80f; // Maximum allowed height for the camera

    public Transform objectToCenterOn; // Object to center on when space is pressed

    private Camera cam;
    private bool isSpacePressed = false; // To track if space is pressed

    private void Start()
    {
        cam = GetComponent<Camera>(); // Get the Camera component attached to this object
    }

    void Update()
    {
        objectToCenterOn = GameObject.Find("RedPlayer").transform;

        HandleMovement();
        HandleScrollZoom();
        HandleRotation();
        HandleCenterOnObject();
    }

    void HandleMovement()
    {
        Vector3 direction = Vector3.zero;

        // Keyboard input (WSAD or Arrow keys)
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
            direction += transform.forward;
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
            direction -= transform.forward;
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            direction += transform.right;
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            direction -= transform.right;

        // Normalize the direction vector to ensure consistent movement speed in all directions
        direction.y = 0; // Prevent movement in the Y direction
        direction.Normalize();

        // Move the camera
        transform.position += direction * panSpeed * Time.deltaTime;

        // Mouse drag input
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            float mouseX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            transform.position -= transform.right * mouseX;
            transform.position -= transform.forward * mouseY;
        }
    }

    void HandleScrollZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Ray from the mouse pointer
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 directionToTarget = hit.point - transform.position; // Calculate the direction to the hit point
                float distance = directionToTarget.magnitude; // Distance to the hit point

                // Non-linear zoom speed adjustment
                float zoomAmount = scroll * scrollSpeed * Mathf.Pow(distance, nonLinearFactor) * Time.deltaTime;

                // Calculate the new camera position
                Vector3 newCameraPosition = transform.position + directionToTarget.normalized * zoomAmount;

                // Clamp the Y position to ensure the camera doesn't go below minY or above maxY
                newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, minY, maxY);

                // Lerp to the new position for smooth zoom
                transform.position = Vector3.Lerp(transform.position, newCameraPosition, zoomSmoothness * Time.deltaTime);
            }
        }
    }

    void HandleRotation()
    {
        // Right mouse button rotation
        if (Input.GetMouseButton(1))
        {
            float rotateX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
            float rotateY = -Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime; // Invert Y for intuitive rotation

            // Apply rotation to the camera
            transform.RotateAround(transform.position, Vector3.up, rotateX);
            transform.RotateAround(transform.position, transform.right, rotateY);
        }

        // Q/E keys rotation around the object
        if (objectToCenterOn != null)
        {
            if (Input.GetKey("q"))
            {
                transform.RotateAround(objectToCenterOn.position, Vector3.up, -rotateSpeed * Time.deltaTime);
            }
            if (Input.GetKey("e"))
            {
                transform.RotateAround(objectToCenterOn.position, Vector3.up, rotateSpeed * Time.deltaTime);
            }
        }
    }

    void HandleCenterOnObject()
    {
        if (Input.GetKey(KeyCode.Space) && objectToCenterOn != null)
        {
            if (!isSpacePressed)
            {
                isSpacePressed = true;

                // Calculate the vector from the object to the origin (0, 0, 0) on the XZ plane
                Vector3 targetPosition = objectToCenterOn.position;
                Vector3 directionToCenter = (Vector3.zero - targetPosition).normalized;

                // Calculate the new camera position closer to the object than the center
                float distanceFromObject = 20f; // Set a fixed distance closer to the object
                Vector3 newCameraPosition = objectToCenterOn.position - directionToCenter * distanceFromObject;

                // Set the Y position to the fixed height
                newCameraPosition.y = fixedHeight;

                // Apply the new camera position
                transform.position = newCameraPosition;

                // Make the camera look at the object
                transform.LookAt(objectToCenterOn);
            }
        }
        else
        {
            isSpacePressed = false; // Reset when space is released
        }
    }
}
