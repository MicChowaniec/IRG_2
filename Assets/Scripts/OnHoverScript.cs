using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics.SymbolStore;

public class OnHoverScript: MonoBehaviour
{
    private string label;
    private string description;
    
    private string forStarlingText;
    private bool button;

    private bool forStarling;
    public OnHoverSC onHoverSC;
    public Material litMaterial;

    public static event Action< string, string,bool> OnHoverBroadcast;

    public void OnEnable()
    {
        StarlingSkillScript.BirdActive += ForStarling;
    }
    public void OnDisable()
    {
        StarlingSkillScript.BirdActive -= ForStarling;
    }

    public void ForStarling(bool b)
    {
        forStarling = b;
    }
    public void OnMouseEnter()
    {
        onHoverSC.AskForDetails();
        label = onHoverSC.label;
        description = onHoverSC.description;
        button = onHoverSC.button;
        if (forStarling)
        {
           
        }
        else
        {
            OnHoverBroadcast?.Invoke(label, description, button);
        }


        Debug.Log("send info to PopUP");
        Highlight();
    }

    public void Highlight()
    {
        Material[] materials = new Material[2];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        materials[1] = litMaterial;
        GetComponent<MeshRenderer>().materials = materials;
    }
    public void StopHighlight()
    {


        Material[] materials = new Material[1];
        materials[0] = this.GetComponent<MeshRenderer>().materials[0];
        GetComponent<MeshRenderer>().materials = materials;
    }
 
    private void OnMouseExit()
    {
        
        StopHighlight();
    }



}
