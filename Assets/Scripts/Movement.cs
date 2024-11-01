using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour, IMovement
{
    public float speed;
    protected Vector3 startingPosition;
    public Vector3 destinationPosition;
    
    public void Update() { }

    public void Move(Vector3 destination)
    {
        StartCoroutine(MoveCoroutine(destination));
    }
    protected IEnumerator MoveCoroutine(Vector3 destination)
    {

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Reached the Destination");

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);


    }
}
