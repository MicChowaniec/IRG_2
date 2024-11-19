using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class OnClickTile
{
    public Material litMaterial;
    private void Highlight()
    {
        Material[] materials = new Material[2];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        materials[1] = litMaterial;
        this.GetComponent<MeshRenderer>().materials = materials;
    }
    private void OnMouseExit()
    {

        Material[] materials = new Material[1];
        materials[0] = this.GetComponent<MeshRenderer>().material;
        this.GetComponent<MeshRenderer>().materials = materials;
    }

    private void OnMouseDown()
    {
        
    }


} 

