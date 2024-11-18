using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement towards the target parent

    private bool shouldMove = false;
    private Transform targetGrandparentTransform; // Parent of SkillTrigger GameObject

    private void OnEnable()
    {
        // Subscribe to the Skill event
        EventTrigger.Skill += OnSkillActivated;

        // Find the SkillTrigger component in the scene (assuming there's only one SkillTrigger)
        EventTrigger skillTrigger = FindFirstObjectByType<EventTrigger>();

        // Set the target parent transform to the parent of SkillTrigger
        if (skillTrigger != null && skillTrigger.transform.parent != null)
        {
            targetGrandparentTransform = skillTrigger.transform.parent.parent;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from the Skill event
        EventTrigger.Skill -= OnSkillActivated;
    }

    private void Update()
    {
        // Move towards the target parent if the flag is set
        if (shouldMove && targetGrandparentTransform != null)
        {
            MoveTowardsParent();
        }
    }

    private void OnSkillActivated()
    {
        // Set the flag to start moving
        shouldMove = true;
        Debug.Log("Skill event received, starting movement towards SkillTrigger's parent!");
    }

    private void MoveTowardsParent()
    {
        // Calculate the step size based on speed and frame time
        float step = moveSpeed * Time.deltaTime;

        // Move the object towards the target parent
        transform.position = Vector3.MoveTowards(transform.position, targetGrandparentTransform.position, step);

        // Check if the object has reached the target parent
        if (Vector3.Distance(transform.position, targetGrandparentTransform.position) < 0.01f)
        {
            shouldMove = false; // Stop moving once close enough
            Debug.Log("Reached SkillTrigger's parent object!");
        }
    }
}
