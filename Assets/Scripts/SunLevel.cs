using UnityEditor;
using UnityEngine;
using System;

public class SunLevel : MonoBehaviour
{
    public GameSettings gameSettings;

    public Light Light;
    public int solarPointer;
    private int signature;
   
    public float dayStep;
    public float nightStep;

    public static event Action<int> DayEvent;
    public static event Action NightEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnNewGame()
    {

        dayStep = 160.0f / (float)gameSettings.CyclesPerDay / 36;
        nightStep = -45;

        MapManager.MapGenerated -= OnNewGame;
        SunRise();
    }
    private void OnEnable()
    {
        MapManager.MapGenerated += OnNewGame;
         PlayerManager.ActivePlayerBroadcast+= SolarUpdate;
        
    }
    private void OnDisable()
    {

        PlayerManager.ActivePlayerBroadcast -= SolarUpdate;
        MapManager.MapGenerated -= OnNewGame;
    }
    private void SunRise()
    {
        solarPointer = 0;
        Light.color = new Color(0, 0.8f, 0.9f, 1);
    }
    private void SunSet()
    {
        solarPointer = -90;
        NightEvent?.Invoke();

        Light.color = new Color(0, 0.3f, 0.6f, 1);
        
        float x = solarPointer;
        Light.transform.rotation = Quaternion.Euler(new Vector3(x, x, 0));
    }


    public void SolarUpdate(int id)
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
        if (solarPointer > 1 && solarPointer < 179)
        {   float x = Mathf.Sin(solarPointer*Mathf.PI/180)*90;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
            DayEvent?.Invoke((int)x);
        }
        else
        {
            SunSet();
        }
    }
}
