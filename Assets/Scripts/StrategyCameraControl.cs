using UnityEngine; // Import the Unity Engine namespace for MonoBehaviour and related classes
using System.Collections;
using Unity.VisualScripting;

public class StrategyCameraControl : MonoBehaviour // Define a public class named StrategyCameraControl inheriting from MonoBehaviour
{
    public float panSpeed = 20f; // Speed of panning the camera
    public float scrollSpeed = 10f; // Base speed of scrolling for moving the camera position
    public float rotateSpeed = 100f; // Speed of rotation for the camera
    public float fixedHeight = 4.23f; // Fixed height for the camera when centering on an object
    public float zoomSmoothness = 5f; // Smoothness factor for zooming in/out
    public float spaceMoveHeight = 10f; // Height to move up when space is pressed
    public float nonLinearFactor = 1.5f; // Factor for non-linear zoom scaling
    public float minY = 5f; // Minimum allowed height for the camera
    public float maxY = 80f; // Maximum allowed height for the camera

    public Transform objectToCenterOn; // Reference to the object the camera will center on when space is pressed

    private Camera cam; // Reference to the Camera component
    private bool isSpacePressed = false; // Boolean to track if the space key is currently pressed


    /// <summary>
    /// Called when the script is first initialized.
    /// Retrieves and stores the reference to the Camera component attached to the GameObject.
    /// </summary>
    private void Start()
    {
        cam = GetComponent<Camera>(); // Get the Camera component attached to this object
    }
    private void OnEnable()
    {
        PlayerManager.ActivePlayerBroadcast += ChangeObjectToCenterOn;
    }
    private void OnDisable()
    {
        PlayerManager.ActivePlayerBroadcast -= ChangeObjectToCenterOn;
    }
    public void ChangeObjectToCenterOn(Player player)
    {
        objectToCenterOn = player.GetComponent<Transform>();
    }

    /// <summary>
    /// Updates every frame.
    /// Calls the movement, zoom, rotation, and centering functions to handle player input and adjust the camera.
    /// </summary>
    void Update()
    {
        HandleMovement();
        HandleScrollZoom();
        HandleRotation();
        HandleCenterOnObject();
    }

    /// <summary>
    /// Handles camera panning and movement based on keyboard and mouse input.
    /// Processes keyboard input (W, A, S, D or Arrow keys) for directional panning and middle mouse button dragging for camera movement.
    /// </summary>
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

    /// <summary>
    /// Controls zooming in and out using the mouse scroll wheel.
    /// Calculates zoom speed and direction based on the distance to the hit point and applies a smooth zoom effect.
    /// </summary>
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

    /// <summary>
    /// Handles camera rotation around its axis or around a specified object.
    /// Uses the right mouse button and Q/E keys to rotate the camera around its own axis or around the objectToCenterOn if it exists.
    /// </summary>
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

    /// <summary>
    /// Centers the camera on a specific object when the space key is pressed.
    /// Moves and adjusts the camera's position and orientation to center on the activePlayer or specified object when space is pressed.
    /// </summary>
    void HandleCenterOnObject()
    {
        // Check if tbs is assigned and has an activePlayer
        

        if (Input.GetKey(KeyCode.Space) && objectToCenterOn != null)
        {
            if (!isSpacePressed)
            {
                isSpacePressed = true;

                // Calculate the vector from the object to the origin (0, 0, 0) on the XZ plane
                Vector3 targetPosition = objectToCenterOn.position;
                Vector3 directionToCenter = (Vector3.zero - targetPosition).normalized;

                // Calculate the new camera position closer to the object than the center
                float distanceFromObject = 5.5f; // Set a fixed distance closer to the object
                Vector3 newCameraPosition = objectToCenterOn.position - directionToCenter * distanceFromObject;

                // Set the Y position to the fixed height
                newCameraPosition.y = fixedHeight;

                // Animate the camera's position
                StartCoroutine(AnimateCameraToPosition(newCameraPosition, objectToCenterOn.position));
            }
        }
        else
        {
            isSpacePressed = false; // Reset when space is released
        }
    }
    /// <summary>
    /// Immediately centers the camera on a specific object without requiring a key press.
    /// Moves and adjusts the camera's position and orientation to center on the activePlayer or specified object.
    /// </summary>
    public void CenterOnObject(GameObject gameObject)
    {
        // Check if tbs is assigned and has an activePlayer
        objectToCenterOn = gameObject.transform;

        if (objectToCenterOn != null)
        {
            // Calculate the vector from the object to the origin (0, 0, 0) on the XZ plane
            Vector3 targetPosition = objectToCenterOn.position;
            Vector3 directionToCenter = (Vector3.zero - targetPosition).normalized;

            // Calculate the new camera position closer to the object than the center
            float distanceFromObject = 5.5f; // Set a fixed distance closer to the object
            Vector3 newCameraPosition = objectToCenterOn.position - directionToCenter * distanceFromObject;

            // Set the Y position to the fixed height
            newCameraPosition.y = fixedHeight;

            // Animate the camera's position
            StartCoroutine(AnimateCameraToPosition(newCameraPosition, objectToCenterOn.position));
        }
    }

    /// <summary>
    /// Coroutine to smoothly animate the camera's position and rotation to the target position and look at the target object.
    /// </summary>
    /// <param name="targetPosition">The position to move the camera to.</param>
    /// <param name="lookAtTarget">The position of the object to look at.</param>
    /// <returns></returns>
    private IEnumerator AnimateCameraToPosition(Vector3 targetPosition, Vector3 lookAtTarget)
    {
        float duration = 1.0f; // Duration of the animation in seconds
        float elapsedTime = 0f; // Time elapsed since the animation started

        Vector3 startingPosition = transform.position; // The starting position of the camera
        Quaternion startingRotation = transform.rotation; // The starting rotation of the camera

        // Calculate the target rotation to look at the object
        Quaternion targetRotation = Quaternion.LookRotation(lookAtTarget - targetPosition);

        while (elapsedTime < duration)
        {
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Lerp the camera's position from the starting position to the target position
            transform.position = Vector3.Lerp(startingPosition, targetPosition, elapsedTime / duration);

            // Lerp the camera's rotation from the starting rotation to the target rotation
            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, elapsedTime / duration);

            yield return null; // Wait for the next frame
        }

        // Ensure the camera reaches the target position and rotation at the end
        transform.position = targetPosition;
        transform.rotation = targetRotation;
    }
}
