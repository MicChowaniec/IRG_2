using System.Collections;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

[CreateAssetMenu(fileName = "TileHoverSC", menuName = "Dynamic/TileOnHover")]
public class TileHoverSC : OnHoverSC
{
    public TileScriptableObject tso;


    public override void AskForDetails()
    {
        tso = this.GetComponent<TileScript>().TSO;
        label = tso.id.ToString();
        description = tso.stander.ToString() +
            "\n Owner: " + tso.owner.ToString() +
            "\n Passable: " + tso.passable.ToString() +
            "\n Rootable: " + tso.rootable.ToString() +
            "\n Stander: " + tso.stander.ToString();
        forStarlingText = ForStarlingText();
        button = false;
    }
    public string ForStarlingText()
    {
        string temp= string.Empty;
        switch (tso.childType)
        {
            case GameObjectTypeEnum.Rock:
                temp = "Put a nest here, to enlarge the field of view";
                break;

            case GameObjectTypeEnum.Bush:
                temp = "Pick some small fruits, of type: " +tso.childColor.ToString();
                break;
            case GameObjectTypeEnum.Water:
                temp = "Hunt some small fish";
                break;
            case GameObjectTypeEnum.Tree:
                    temp = "Pick fruit from this tree, type: "+tso.childColor.ToString();
                break;
        }

        return temp;
    }
   

}
