using System.Collections.Generic;
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

    public override string AskForDetails()
    {
        label = tileTypes.ToString();
        string standerStr = "";
        string ownerStr = "";

        if (stander != null)
        {
            standerStr = stander.itsName;
        }
        if (owner != null)
        {
            ownerStr=owner.itsName;
        }

      
       
        description =
        "Stander: " + standerStr +
        "\n Owner: " + ownerStr +
        "\n Passable: " + passable.ToString() +
        "\n Rootable: " + rootable.ToString() +
        "\n Object: " + childType.ToString();

        button = false;
        return description;
    }
    public override GameObjectTypeEnum GetChildObjectType()
    {
        return childType;
    }
    public override ActionTypeEnum GetChildObjectColor()
    {
        return childColor;
    }
    public override Vector3 GetPosition()
    {
        return coordinates;
    }
    public override Player GetStander()
    {
        return stander;
    }
    public void SetStander(Player player)
    {
        stander = player;
    }
    public override Player GetOwner()
    {
        return owner;
    }
    public void SetOwner(Player player)
    {
        owner = player;
    }
    public void EstimateValue(int i)
    {
        value += i;
    }
}
