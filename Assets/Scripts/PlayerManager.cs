using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerManager: MonoBehaviour 
{
    public TurnBasedSystem tbs;
    public Player[] players;
    public void AllocatePlayers()
    {

        foreach (Player p in players)
        {

            GameObject player = Instantiate(p.Prefab, p.startPos, p.startRot);
            player.name = p.name;
            player.GetComponent<PlayerScript>().seqId = p.sequence;
            player.GetComponent<MeshRenderer>().material = p.material;
            player.GetComponent<PlayerScript>().treePrefab = p.TreePrefab;
            player.GetComponent<PlayerScript>().human = p.human;
            tbs.pickedPlayer = player;
            tbs.players.Add(player);



        }
        tbs.Prepare();
    }
}
