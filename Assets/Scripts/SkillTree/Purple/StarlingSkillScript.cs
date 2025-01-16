using UnityEngine;
using System;

public class StarlingSkillScript : AbstractSkill
{
    public GameObject StarlingPrefab;
    public Player activePlayer;

    public Camera mainCamera; // Assign your camera in the Inspector
    public float offset = 0.5f; // Vertical offset for the object's position
    public float followSpeed = 5f; // Speed of movement towards the hit position
    public float rotationSpeed = 5f; // Speed of rotation towards the destination

    public override void Do(int range, bool self)
    {
        mainCamera = FindAnyObjectByType<Camera>();
        
        StatisticChange(1, 0, 0, 0, 0, 0, 0);
    }
    public void ClickOnButton()
    {
        Instantiate(StarlingPrefab, transform.position, Quaternion.identity);
        Cursor.visible = false; // Hide the cursor

        
    }
    public override void StatisticChange(int starling, int biomass, int water, int energy, int protein, int resistance, int eyes)
    {
        
    }
}
