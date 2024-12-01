using TMPro;
using UnityEngine;

public class LabelForSliderScript : MonoBehaviour
{
    private TextMeshProUGUI myTextMeshPro;
    [HideInInspector]
    public int m_Value;

    void Start()
    {
        myTextMeshPro = this.GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void OnValueChanged(int value)
    {
        m_Value = value;
        UpdateText();
    }

    void UpdateText()
    {
        myTextMeshPro.text = m_Value.ToString(); // Converts integer to string
    }

}
