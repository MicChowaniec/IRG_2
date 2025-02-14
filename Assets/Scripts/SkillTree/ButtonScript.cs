using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour 
{
    public OnHoverSC skill;

   
    public void OnClick()
    {
        skill = FindAnyObjectByType<OnHoverSC>();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
 
}
