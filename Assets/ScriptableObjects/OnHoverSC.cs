using UnityEngine;


public abstract class OnHoverSC : ScriptableObject
{
    public string label;
    public string description;
    public bool button;
    public virtual void AskForDetails()
    {

    }
}
