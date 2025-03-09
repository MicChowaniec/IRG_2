
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
    public Player stander;
    public Player owner;
    public TreeSO treeSO;
    public GameObjectTypeEnum gote;
    public ActionTypeEnum ate;
    public Vector3 position;

    public virtual string Label()
    {
        return "";
    }
    public virtual string Descripton()
    {
        return "";
    }

    public GameObjectTypeEnum GetChildObjectType()
    {
        return gote;

    }
    public ActionTypeEnum GetChildObjectColor()
    {
        return ate;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public Player GetStander()
    {
        
        return stander;
    }
    public TreeSO GetStandingTree()
    {
        return treeSO;
    }
    public Player GetOwner()
    {
        return owner;
    }
    

}
