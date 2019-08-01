using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// flight path
public class MovePattern : MonoBehaviour
{
    // instructions
    public Vector2[] directions;
    public float[] times;

    public Moving myMoves;
    public Waiting myWait;

    // index of instruction
    int current;

    // bool for moving
    bool isMoving;
    bool once;

    // user properties
    public float moveSpeed;

    private void Start()
    {
        isMoving = true;
        once = true;
        moveSpeed = 0.2f;
    }


    private void Update()
    {
        if (isMoving)
        {
            // start timer for instruction
            if (once)
            {
                StartCoroutine(Timer(myMoves, myMoves.timings[myMoves.counter]));
            }
            Vector2 myPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, myPos + myMoves.moves[myMoves.moveCount], moveSpeed);
        }
        else
            if(once)
                StartCoroutine(Timer(myWait, myWait.timings[myWait.counter]));
    }

    IEnumerator Timer(Waiting task, float secs)
    {
        once = false;
        yield return new WaitForSeconds(secs);
        isMoving = SwitchBool(isMoving);
        // loop through array
        if (task.counter < task.timings.Length-1)
        {
            task.counter++;
            Debug.Log(task + " " + task.counter);   
        }
        else
        {
            task.counter = 0;
            Debug.Log(task + " " + task.counter);
        }

        once = true;
    }


    IEnumerator Timer(Moving task, float secs)
    {
        once = false;
        yield return new WaitForSeconds(secs);
        isMoving = SwitchBool(isMoving);
        // loop through array
        if (task.moveCount < task.moves.Length - 1)
        {
            task.moveCount++;
            Debug.Log(task + ": " + task.moveCount);
        }
        else
        {
            task.moveCount = 0;
            Debug.Log(task + ": " + task.moveCount);
        }

        once = true;
    }

    bool SwitchBool(bool b)
    {
        if (b == true)
        {
            b = false;
        }
        else
            b = true;

        return b;
    }

 
}



// for use in the inspector
[System.Serializable]
public class Waiting
{
    public int counter;
    public float[] timings;
}

[System.Serializable]
public class Moving : Waiting
{
    public Vector2[] moves;
    public int moveCount;
}

