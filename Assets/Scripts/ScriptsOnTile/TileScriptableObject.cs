using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Tile_SO", menuName = "Dynamic/Tile_SO")]
public class TileScriptableObject : OnHoverSC
{
    public int id;
    public bool passable;
    public bool rootable;
    public Vector3 coordinates;
    public Vector2 ijCoordinates;
    public GameObject representation;
    public TileTypesEnum tileTypes;
    public GameObjectTypeEnum childType;
    public ActionTypeEnum childColor;
    public int value;

   
    public List<TileScriptableObject> neighbours = new();

    public override string Label()
    {
        Debug.Log("StartedCreatingText");
        label = tileTypes.ToString();
      
        return label;
    }
    public override string Descripton()
    {
        string standerStr = "";
        string ownerStr = "";

        if (stander != null)
        {
            standerStr = stander.itsName;
        }
        if (owner != null)
        {
            ownerStr = owner.itsName;
        }
        description =
        "Stander: " + standerStr +
        "\n Owner: " + ownerStr +
        "\n Passable: " + passable.ToString() +
        "\n Rootable: " + rootable.ToString() +
        "\n Object: " + childType.ToString();

        return description; ;
    }



    public void SetStander(Player player)
    {
        stander = player;
        if (player != null)
        {
            childType = GameObjectTypeEnum.Player;
            passable = false;
        }
      


    }
    public void RemoveStander()
    {
        stander = null;
        if (childType != GameObjectTypeEnum.Tree)
        {
            childType = GameObjectTypeEnum.None;
            passable = true;
        }
    }

    public void SetOwner(Player player)
    {
        owner = player;
    }
    public void EstimateValue(int i)
    {
        value += i;
    }

    public void MakeVisibleForSpectator()
    {
        representation.layer = 6;
    }
}
