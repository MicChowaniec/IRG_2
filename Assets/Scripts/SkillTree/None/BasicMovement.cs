using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicMovement : AbstractSkill
{
    public Vector3 currentPosition;
    public GameObject activePlayerObject;

    public Vector3 destination;
    private Coroutine movementCoroutine;
    public PlayerManager pm;
    public TileScriptableObject tileWherePlayerStands;


    public void LateUpdate()
    {
        if (activePlayerObject == null && activePlayer != null)
        {
            FindPlayerTransform();
        }
    }
    public void FindPlayerTransform()
    {
        Debug.Log("FindPlayerTransform: Wywo³ano funkcjê.");

        if (pm == null)
        {
            Debug.LogError("FindPlayerTransform: pm jest nullem!");
            return;
        }

        if (activePlayer == null)
        {
            Debug.LogError("FindPlayerTransform: activePlayer jest nullem!");
            return;
        }

        GameObject playerObj = pm.GetGameObjectFromSO(activePlayer);
        if (playerObj == null)
        {
            Debug.LogError("FindPlayerTransform: Nie znaleziono obiektu gracza dla activePlayer!");
            return;
        }

        activePlayerObject = playerObj;
        Debug.Log("FindPlayerTransform: Ustawiono activePlayerObject.");

        currentPosition = activePlayerObject.transform.position;
        destination = currentPosition;

        PlayerScript playerScript = activePlayerObject.GetComponent<PlayerScript>();

        if (playerScript == null)
        {
            Debug.LogError("FindPlayerTransform: Brak komponentu PlayerScript na activePlayerObject!");
            return;
        }

        tileWherePlayerStands = playerScript.tile;
        Debug.Log("FindPlayerTransform: Znaleziono i przypisano tileWherePlayerStands.");
    }
    public override void Do (TileScriptableObject tileScriptableObject)
    {
        if (tileScriptableObject.passable)
        {
            Debug.Log("Tile is Passable");
            if (activePlayer.human)
            {
                Debug.Log("PlayerIsHuman");

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
        if (activePlayerObject == null)
        {
            FindPlayerTransform();
            yield break;
        }
        Transform transformOfPlayer = activePlayerObject.transform;
        transformOfPlayer.LookAt(destination, Vector3.up);
        activePlayerObject.GetComponent<Animator>().SetTrigger("Move");

        while (Vector3.Distance(transformOfPlayer.position, destination) > 0.1f)
        {
            transformOfPlayer.position = Vector3.MoveTowards(transformOfPlayer.position, destination, Time.deltaTime * 5f);
            yield return null;
        }
        activePlayerObject.GetComponent<Animator>().SetTrigger("Idle");
        transformOfPlayer.position = destination;
        movementCoroutine = null;
        DisableFunction();
    }


}



