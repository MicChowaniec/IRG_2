using UnityEngine;

[CreateAssetMenu(fileName = "Tile_SO", menuName = "Dynamic/Tile_SO")]
public class TileScriptableObject : OnHoverSC
{
    public int id;
    public bool passable;
    public bool rootable;
    public Vector3 coordinates;
    public Vector2 ijCoordinates;
    public Player owner;
    public Player stander;
    public GameObject representation;
    public TileTypesEnum tileTypes;
    public GameObjectTypeEnum childType;
    public ActionTypeEnum childColor;

    public override void AskForDetails()
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
    }
    public override GameObjectTypeEnum GetChildObjectType()
    {
        return childType;
    }
    public override ActionTypeEnum GetChildObjectColor()
    {
        return childColor;
    }
}
