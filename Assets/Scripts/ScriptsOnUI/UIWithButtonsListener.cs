using UnityEngine;
using UnityEngine.EventSystems;

public class UIWithButtonsListener : MonoBehaviour, IPointerExitHandler
{
   
    private void OnEnable()
    {
        // Subscribe to the Skill event
        OnClickTile.OnClick += EnableAllChildren;

    }

    private void OnDisable()
    {
        // Unsubscribe from the Skill event
        OnClickTile.OnClick -= EnableAllChildren;
    }

  

    private void EnableAllChildren(Vector3 position)
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.position = position;
    }
    public void DisableAllChildren()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void SetPositionToPointer(Vector2 mousePosition)
    {
        mousePosition = Input.mousePosition;
        this.transform.position = mousePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisableAllChildren();
    }


      

}
