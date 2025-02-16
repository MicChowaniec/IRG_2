using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonScript : MonoBehaviour
{
    public OnHoverSC skill;
    public ActionManager ActionManager;

    public void Start()
    {
        ActionManager = FindAnyObjectByType<ActionManager>();

        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    public virtual void OnClick()
    {
        
    }
    
    
 
}
