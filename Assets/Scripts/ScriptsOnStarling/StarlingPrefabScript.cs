using TMPro;
using UnityEngine;

public class StarlingPrefabScript : MonoBehaviour
{
    
    private Camera mainCamera; // Assign your camera in the Inspector
    public float offset = 0.5f; // Vertical offset for the object's position
    public float followSpeed = 5f; // Speed of movement towards the hit position
    public float rotationSpeed = 5f; // Speed of rotation towards the destination
    private PlayerManager playerManager;
    private TileScriptableObject AIdestination;

    void Start()
    {
        
        playerManager = FindAnyObjectByType<PlayerManager>();
        mainCamera = FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        if (playerManager.activePlayer.human)
        {
            if (mainCamera == null)
            {
                mainCamera = FindAnyObjectByType<Camera>();
            }


            // Create a ray from the camera through the mouse position

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits an object in the scene
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is the object itself or any of its children
                if (hit.collider.transform == transform || hit.collider.transform.IsChildOf(transform))
                {
                    return; // If the ray hits the object or its children, do nothing
                }

                // Get the hit point
                Vector3 targetPosition = hit.point;

                // Add the offset (e.g., vertically upwards)
                targetPosition += new Vector3(0, offset, 0);

                // Gradually move the object towards the target position
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

                // Rotate the object to face the destination
                Vector3 direction = (targetPosition - transform.position).normalized; // Direction towards the destination
                if (direction != Vector3.zero) // Avoid rotation errors for zero direction
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (AIdestination != null)
            {
                AIStarlingMovememnt(AIdestination);
            }
        }
    }
    public void AIStarlingMovememnt(TileScriptableObject tso)
    {
        tso = AIdestination;
        Vector3 targetPosition = new Vector3(0, offset, 0) + tso.coordinates;
        // Gradually move the object towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Rotate the object to face the destination
        Vector3 direction = (targetPosition - transform.position).normalized; // Direction towards the destination
        if (direction != Vector3.zero) // Avoid rotation errors for zero direction
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
