using UnityEngine;

public class SunLevel : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameObject dayUI;
    public GameObject nightUI;

    public Light Light;
    private int solarPointer;
    private int solarMaximum;
    private int signature;
   
    public float step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        step = 160.0f / gameSettings.CyclesPerDay;
        solarPointer = 10;
        solarMaximum = 90;
        SolarRise();
    }
    private void OnEnable()
    {
        TurnBasedSystem.NextTurn += SolarUpdate;
    }
    private void OnDisable()
    {
        TurnBasedSystem.NextTurn -= SolarUpdate;
    }

    public void SolarRise()
    {
        dayUI.SetActive(true);
        nightUI.SetActive(false);
        SolarUpdate();
    }
    public void SolarUpdate()
    {
        solarPointer += (int)step;
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
        if (solarPointer > 10 && solarPointer< 170)
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
