using System.Reflection.Emit;
using Unity.VisualScripting;
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
        button = false;
    }


}
