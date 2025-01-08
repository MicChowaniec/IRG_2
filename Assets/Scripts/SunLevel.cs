using UnityEditor;
using UnityEngine;
using System;

public class SunLevel : MonoBehaviour
{
    public GameSettings gameSettings;

    public Light Light;
    private int solarPointer;
    private int solarMaximum;
    private int signature;
   
    public float dayStep;
    public float nightStep;

    public static event Action<int> DayEvent;
    public static event Action NightEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnNewGame()
    {

        dayStep = 180.0f / (float)gameSettings.CyclesPerDay;
        nightStep = -45;
        solarPointer = 10;
        solarMaximum = 90;
        MapManager.MapGenerated -= OnNewGame;
    }
    private void OnEnable()
    {
        MapManager.MapGenerated += OnNewGame;
        TurnBasedSystem.NextTurn += SolarUpdate;
        
    }
    private void OnDisable()
    {

        TurnBasedSystem.NextTurn -= SolarUpdate;
    }

   
    public void SolarUpdate()
    {

        solarPointer += (int)dayStep;
        //Sun position check for zenit during year - TODO
        //if (signature != -1 && solarMaximum < 90)
        //{
         //   signature = 1;
        //}
        //else
        //{
        //    signature = -1;
        //}
//
        //solarMaximum += 1 * signature;
   

      //Sun position funtion during day/night
        if (solarPointer > 10 && solarPointer< 170)
        {
            float x = Mathf.Abs((float)solarPointer - 1 / 2 * solarMaximum) - 1 / 2 * solarMaximum;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
            Light.color = new Color(0, 0.8f, 0.9f, 1);
            DayEvent?.Invoke((int)x);
           


        }
        else
        {
            solarPointer = -90;
            float x = Mathf.Abs((float)solarPointer - 1 / 2 * solarMaximum) - 1 / 2 * solarMaximum;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
            Light.color = new Color(0,0.3f,0.6f,1);


            NightEvent?.Invoke();
        }
    }
}
