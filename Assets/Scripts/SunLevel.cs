using UnityEditor;
using UnityEngine;
using System;
using TMPro;

public class SunLevel : MonoBehaviour
{
    public GameSettings gameSettings;
    public ScoreKeeperScript scoreKeeperScript;

    public GameObject winnerPanel;
    public TextMeshProUGUI winnerText;

    public Light Light;
    public int solarPointer;
    private int signature;
   
    public float dayStep;
    public float nightStep;




    public static event Action<int> DayEvent;
    public static event Action NightEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        Entity.Death += SunSet;
        solarPointer = 0;
        dayStep = 180.0f / ((float)gameSettings.SizeOfMap * 6.0f);
        SunRise();

        PlayerManager.ChangePhase += SolarUpdate;
        scoreKeeperScript = FindAnyObjectByType<ScoreKeeperScript>();
        

    }
    private void OnDisable()
    {

        PlayerManager.ChangePhase -= SolarUpdate;
        Entity.Death -= SunSet;

    }
    private void SunRise()
    {
        solarPointer = 1;
        Light.color = new Color(1, 0.8f, 0.9f, 1);

    }
    private void SunSet(string stringer)
    {
        solarPointer = -90;
        NightEvent?.Invoke();

        Light.color = new Color(0, 0.3f, 0.6f, 1);
        
        float x = solarPointer;
        Light.transform.rotation = Quaternion.Euler(new Vector3(x, x, 0));
        winnerPanel.SetActive(true);
        winnerText.text = stringer;

    }


    public void SolarUpdate()
    {

        solarPointer += (int)dayStep;
        //Sun position check for zenit during year - TODO
        if (signature != -1 && solarPointer < 89)
        {
           signature = 1;
        }
        else
        {
            signature = -1;
        }
//
        //solarMaximum += 1 * signature;
   

      //Sun position funtion during day/night
        if (solarPointer > 0 && solarPointer < 181)
        {   float x = Mathf.Sin((float)solarPointer*Mathf.PI/180.0f)*90.0f;
            float y = (float)solarPointer / 2.0f;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
            DayEvent?.Invoke((int)x);
        }
        else
        {
            SunSet("Winner is+" + scoreKeeperScript.winner);
        }
    }
}
