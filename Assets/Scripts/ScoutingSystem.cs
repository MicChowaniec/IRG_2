using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoutingSystem : MonoBehaviour
{
    public static Action OnScoutingSystem;

    public GameObject StarlingPrefab;
    public bool canYouFly = false;
    private GameObject starling;
    public void onClickPrepareStarling()
    {
        starling = Instantiate(StarlingPrefab);

    }
    


}
