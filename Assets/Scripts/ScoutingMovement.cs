using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoutingMovement : Movement
{
    PlayerMovement pm;
    TurnBasedSystem tbs;

    public void Start()
    {
        tbs = FindAnyObjectByType<TurnBasedSystem>();
        pm = tbs.activePlayer.GetComponent<PlayerMovement>(); 
    }

    public new void Move(Vector3 destination)
    { 
            StartCoroutine(MoveCoroutine(destination));
    }
    public void MoveAndBack(Vector3 destination,Vector3 start)
    {
        StartCoroutine(MoveAndBackCoroutine(destination, start));
    }

    private IEnumerator MoveAndBackCoroutine(Vector3 destination, Vector3 start)
    {

        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Reached the Destination");

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);


        // Move back to the starting position
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPosition, speed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("Returned to Start Position");


        Destroy(this);
    }
    
}
