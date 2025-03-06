using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicMovement : AbstractSkill
{
    private Vector3 position;
    private Vector3 destination;
    private Transform PlayerTransform;


    public void FindPlayerTransform()
    {
        PlayerTransform = FindAnyObjectByType<PlayerManager>().GetTransformFromSO(activePlayer);
    }
    public void Do(TileScriptableObject tileScriptableObject)
    {
        if (PlayerTransform == null)
        {
            FindPlayerTransform();
        }
        if (activePlayer.human)
        {

            TileScriptableObject tileWherePlayerStands = PlayerTransform.GetComponent<PlayerScript>().tile;
            if (tileWherePlayerStands != null && tileWherePlayerStands.neighbours.Contains(tileScriptableObject))
            {
                //transform.position = tileScriptableObject.GetPosition();
            }

        }
        else
        {
            //make sure the tileScriptableObject is withing Movement Range for AI in other way
            position = FindAnyObjectByType<PlayerManager>().GetTransformFromSO(activePlayer).transform.position;
            destination = tileScriptableObject.coordinates;

        }
    }
    public void FixedUpdate()
    {
        if (Vector3.Distance(position, destination) > 0.1f)
        {
            SmoothlyTransform(position, destination);
        }
        else
        {
            PlayerTransform.position = destination;
        }
    }
    public void SmoothlyTransform(Vector3 pointA, Vector3 pointB)
    {
        PlayerTransform.LookAt(pointB, Vector3.up);
        PlayerTransform.position += (pointB - pointA) * Time.deltaTime;
        PlayerTransform.GetComponent<Animator>().SetTrigger("Move");
    }

}
