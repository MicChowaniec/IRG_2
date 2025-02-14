using UnityEngine;


public abstract class OnHoverSC : ScriptableObject
{
    public string label;
    [TextArea(10, 100)]
    public string description;
    [TextArea(10, 100)]
    public string forStarlingText;
    public bool button;
    public Sprite sprite;
    public virtual void AskForDetails()
    {

    }
    public virtual GameObjectTypeEnum GetChildObjectType()
    {
        return GameObjectTypeEnum.None;

    }
    public virtual ActionTypeEnum GetChildObjectColor()
    {
        return ActionTypeEnum.None;
    }
    public virtual Vector3 GetPosition()
    {
        return Vector3.zero;
    }
    
}
