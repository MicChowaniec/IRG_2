using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicMovement : AbstractSkill
{
    public Vector3 currentPosition;

    public Vector3 destination;
    private Coroutine movementCoroutine;
    public PlayerManager pm;


  
    public override void Do (TileScriptableObject tileScriptableObject)
    {
        if (tileScriptableObject.passable)
        {
            Debug.Log("Tile is Passable");
            if (activePlayer.human)
            {
                Debug.Log("PlayerIsHuman");
                if(tileWherePlayerStands == null)
                {
                    PlayerScript playerScript = activePlayerObject.GetComponent<PlayerScript>();
                    tileWherePlayerStands = playerScript.tile;
                }
                if (tileWherePlayerStands != null)
                {
                    Debug.Log("Tile is not Null ");
                    destination = tileScriptableObject.coordinates;
                    if (tileWherePlayerStands.neighbours.Contains(tileScriptableObject))
                    {
                        Debug.Log("Tile is in neigbours");
                        MoveTo(destination);
                    }
                }

            }
            else
            {
                //make sure the tileScriptableObject is withing Movement Range for AI in other way

                destination = tileScriptableObject.coordinates;
                MoveTo(destination);

            }
        }
    }
  

    public void MoveTo(Vector3 newDestination)
    {
        destination = newDestination;

        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
        }

        movementCoroutine = StartCoroutine(SmoothlyMoveToDestination());
    }

    private IEnumerator SmoothlyMoveToDestination()
    {

        Transform transformOfPlayer = activePlayerObject.transform;
        transformOfPlayer.LookAt(destination, Vector3.up);
        activePlayerObject.GetComponent<Animator>().SetTrigger("Move");

        while (Vector3.Distance(transformOfPlayer.position, destination) > 0.1f)
        {
            transformOfPlayer.position = Vector3.MoveTowards(transformOfPlayer.position, destination, Time.deltaTime );
            yield return null;
        }
        activePlayerObject.GetComponent<Animator>().SetTrigger("Idle");
        transformOfPlayer.position = destination;
        movementCoroutine = null;

        StatisticChange();

        Confirm();

        DisableFunction();
    }


}



