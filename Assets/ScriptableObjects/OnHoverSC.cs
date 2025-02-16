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
    protected Player stander;
    protected Player owner;
    protected TreeSO treeSO;
    public virtual string AskForDetails()
    {
        return "";
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
    public virtual Player GetStander()
    {
        return null;
    }
    public virtual TreeSO GetStandingTree()
    {
        if (treeSO != null)
        {
            return treeSO;
        }
        else
        {
            return null;
        }
    }
    public virtual Player GetOwner()
    {
        if (owner != null)
        return owner;
        else
        {
            return null;
        }
    }
    

}
