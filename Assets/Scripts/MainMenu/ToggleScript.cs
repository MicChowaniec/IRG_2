using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ToggleScript : MonoBehaviour
{
    private Toggle myToggle;
    public ParametersScript parametersScript;
    public bool defaultValue;
    void Start()
    {
        myToggle = this.GetComponent<Toggle>();
        myToggle.isOn = defaultValue;

        
        

    }
}
