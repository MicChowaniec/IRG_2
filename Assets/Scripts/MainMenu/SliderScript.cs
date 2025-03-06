using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SliderScript : MonoBehaviour
{
    private Slider mySlider;
    public TextMeshProUGUI myTextMeshPro;
    public string staticTextLabel;
    public ParametersScript parametersScript;
    public int defaultValue;

    void Start()
    {
        mySlider= this.GetComponent<Slider>();
        mySlider.value = defaultValue;
        myTextMeshPro.text= staticTextLabel+ defaultValue.ToString();
        mySlider.wholeNumbers = true; // Ensure slider only outputs integers
        myTextMeshPro.text = staticTextLabel + ((int)mySlider.value).ToString();
        mySlider.onValueChanged.AddListener((value) => {
            myTextMeshPro.text = staticTextLabel+((int)value).ToString(); // Cast and convert
        });
        if (this.name == "Map Size")
        {
            mySlider.onValueChanged.AddListener((value) => { parametersScript.UpdateMapSize((int)value); });
        }
        if (this.name == "CyclesPerDay")
        {
            mySlider.onValueChanged.AddListener((value) => { parametersScript.UpdateCyclesPerDay((int)value); });
        }
        if (this.name == "Difficulty")
        {
            mySlider.onValueChanged.AddListener((value) => { parametersScript.UpdateDifficultyLevel((int)value); });
        }
        

    }


}
