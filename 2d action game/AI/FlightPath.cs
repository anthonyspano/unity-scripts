using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPath : MonoBehaviour
{
    [Range(0,1)]
    public float moveSpeed;

    GameObject player;
    Vector2 myPath;

    // checking distance away from self
    public float inBoundary;
    public float outBoundary;

    public enum Orientation { left, right };
    public Orientation myFace;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(inBoundary, inBoundary/2));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(outBoundary, outBoundary/2));
    }

    private void Start()
    {
        player = GameObject.Find("Oliver");

    }

    private void Update()
    {
        //// if player is outside the boundary
        //if (player.transform.position.x - transform.position.x >= inBoundary / 2)
        //{
        //    // inBoundary right
        //    // go left
        //    // follow preset path backwards
        //    FollowPathBackwards(transform.position);
        //    myFace = Orientation.left;
        //}

        //else if (player.transform.position.x - transform.position.x < -(inBoundary/2))
        //{
        //    // inBoundary left
        //    // go right
        //    transform.position = Vector2.MoveTowards(transform.position, 
        //                                             new Vector2(player.transform.position.x , transform.position.y),
        //                                             moveSpeed);
        //    myFace = Orientation.right;
        //}

        //else if (player.transform.position.x - transform.position.x >= outBoundary / 2)
        //{
        //    // outBoundary right
        //    // follow preset path backwards
        //    FollowPathBackwards(transform.position);
        //}

        //else if (player.transform.position.x - transform.position.x < -(outBoundary / 2))
        //{ 
        //    // outBoundary left
        //    // follow preset path 
        //    FollowPath(transform.position);

        //}

        //if(myFace == Orientation.right)
        //{
        //    FollowPath(transform.position);
        //}

        //if(myFace == Orientation.left)
        //{
        //    FollowPathBackwards(transform.position);
        //}

        //////////////////////////////////////////////////////////////////////////////////////////////////

        // if player is outside the boundary
        if (player.transform.position.x - transform.position.x >= inBoundary / 2)
        {
            // inBoundary left
            // go left
            // follow preset path backwards
            myFace = Orientation.right;
            //ClockHand(-Time.fixedTime);
            
        }

        else if (player.transform.position.x - transform.position.x < -(inBoundary / 2))
        {
            // inBoundary right
            // go right
            myFace = Orientation.left;
            //ClockHand(Time.fixedTime);
            

        }

        else if (player.transform.position.x - transform.position.x >= outBoundary / 2)
        {
            // outBoundary right
            // follow preset path backwards
            myFace = Orientation.left;
            //ClockHand(-Time.fixedTime);

        }

        else if (player.transform.position.x - transform.position.x < -(outBoundary / 2))
        {
            // outBoundary left
            // follow preset path 
            myFace = Orientation.right;
            //ClockHand(Time.fixedTime);
        }

        if (myFace == Orientation.right)
        {
            ClockHand(Time.fixedTime);
        }

        if (myFace == Orientation.left)
        {
            ClockHand(-Time.fixedTime);
        }
    }


    void FollowPath(Vector2 targetPos)
    {
        
        // time counting up
        targetPos.x += Time.fixedTime;
        
        // wide and shallow parabola - y = 1/64(x^2)
        targetPos.y = (Mathf.Pow(targetPos.x, 2) / 40) + transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);
    }

    void FollowPathBackwards(Vector2 targetPos)
    {
        // timer counting down
        targetPos.x -= Time.fixedTime;

        // wide and shallow parabola - y = 1/64(x^2)
        targetPos.y = (Mathf.Pow(targetPos.x, 2) / 40) + transform.position.y;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);
    }


    // ONE FUNCTION
    void ClockHand(float t)
    {
        Vector2 targetPos = new Vector2();
        if (t > 0)
        {
            // Should work both ways
            t += Time.fixedTime;

            // timer counting down
            targetPos.x = t;

            targetPos.y = (Mathf.Pow(targetPos.x, 2) / 40); // + transform.position.y;
        }
        else
        {
            // Should work both ways
            t -= Time.fixedTime;

            // timer counting down
            targetPos.x = t;

            targetPos.y = (Mathf.Pow(targetPos.x, 2) / 40); // + transform.position.y;

        }

        transform.Translate(targetPos);
        //transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);

    }







}
