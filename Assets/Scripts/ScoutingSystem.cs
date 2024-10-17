using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutingSystem : MonoBehaviour
{
    public GameObject StarlingPrefab;
    public bool canYouFly =false;
    public void onClickPrepareStarling()
    {
        canYouFly=true;
    }
}
