using UnityEngine;
using System;
using Unity.VisualScripting;

public class StarlingSkillScript : AbstractSkill
{


    public GameObject StarlingPrefab;
    private GameObject StarlingInstatiated;
    public Player activePlayer;
    public bool birdActive;

    public static event Action<bool> BirdActive;
    public static event Action<int> StarlingConsumed;


    private void OnEnable()
    {
        BirdActive += SetActiveBird;
    }
    private void OnDisable()
    {
        BirdActive -= SetActiveBird;
    }
    private void SetActiveBird(bool b)
    {
        birdActive = b;
    }
    public override void Do(int range, bool self)
    {
        StatisticChange(1, 0, 0, 0, 0, 0, 0);
    }
    public void ClickOnButton()
    {
        if (CheckResources(1))
        {
            StarlingInstatiated = Instantiate(StarlingPrefab, transform.position, Quaternion.identity);
            StarlingInstatiated.GetComponent<VisionSystem>().owner = activePlayer;
            Cursor.visible = false; // Hide the cursor
            BirdActive?.Invoke(true);
        }
    }
    public void Update()
    {
        if (!birdActive) { return; }
        else if (Input.GetMouseButtonDown(1))
            {
            StarlingInstatiated.GetComponent<VisionSystem>().owner = null;
            Destroy(StarlingInstatiated); StarlingInstatiated = null;
            BirdActive?.Invoke(false);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Do(0, false);
            StarlingInstatiated.GetComponent<VisionSystem>().owner = null;
            Destroy(StarlingInstatiated); StarlingInstatiated = null;
            BirdActive?.Invoke(false);
            StarlingConsumed?.Invoke(-1);
        }
    }

    public override void StatisticChange(int starling, int biomass, int water, int energy, int protein, int resistance, int eyes)
    {

    }
    public override bool CheckResources(int res)
    {
        if (activePlayer.starlings>=res)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
