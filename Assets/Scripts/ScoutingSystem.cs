using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoutingSystem : MonoBehaviour
{
    public GameObject StarlingPrefab;
    public bool canYouFly = false;
    private GameObject starling;
    public void onClickPrepareStarling()
    {
        starling = Instantiate(StarlingPrefab);

    }
    public void ClickOnTileToFly(Vector3 destination)
    {
        starling.GetComponent<ScoutingMovement>().Move(destination);
        starling.GetComponent<VisionSystem>().ScanForVisible();
    }


}
