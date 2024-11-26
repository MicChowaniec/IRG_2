using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class SunLevel : MonoBehaviour
{


    public GameObject dayUI;
    public GameObject nightUI;

    public Light Light;
    private int solarPointer;
    private int solarMaximum;
    private int signature;

    [HideInInspector]
    public int step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SolarRise()
    {
        dayUI.SetActive(true);
        nightUI.SetActive(false);
        solarPointer = 1;
    }
    public void SolaUpdate()
    {
        solarPointer += step;
        //Sun position check for zenit during year
        if (signature != -1 && solarMaximum < 90)
        {
            signature = 1;
        }
        else
        {
            signature = -1;
        }

        solarMaximum += 1 * signature;
   

      //Sun position funtion during day/night
        if (solarPointer > 0 && solarPointer< 181)
        {
            float x = Mathf.Abs((float)solarPointer - 1 / 2 * solarMaximum) - 1 / 2 * solarMaximum;
            float y = (float)solarPointer / 2;
            Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));


        }
            else
        { 
        dayUI.SetActive(false);
        nightUI.SetActive(true);
        solarPointer = -90;
        float x = -90;
        float y = (float)solarPointer / 2;
        Light.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
        }
    }
}
