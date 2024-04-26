using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Script : MonoBehaviour
{
    public BaseScript bs;

    public Transform[] waypoints;

    public float moveSpeed = 1f;

    public int waypointIndex = 1;

    public static bool moveAllowed = false;

    public int curindex = 0;

    public static bool rolledDouble3 = false;

    public static int oppPenaltyRem = 2;

    // Start is called before the first frame update
    void Start()
    {
        bs = FindObjectOfType<BaseScript>();

        GameObject waypointsp1 = GameObject.FindGameObjectWithTag("WaypointsP1");
        waypoints = new Transform[40];
        waypoints[0] = waypointsp1.transform.GetChild(0);
        for (int i = 1; i < 40; ++i)
            waypoints[i] = waypointsp1.transform.GetChild(39 - i + 1);

        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveAllowed)
        {
            move();
        }
    }

    public void move()
    {
        if (waypointIndex <= DiceCheckZoneScript.diceSum + curindex)
        {
            transform.position =
                Vector3
                    .MoveTowards(transform.position,
                    waypoints[waypointIndex % waypoints.Length]
                        .transform
                        .position,
                    moveSpeed * Time.deltaTime);

            if (
                transform.position ==
                waypoints[waypointIndex % waypoints.Length].transform.position
            )
            {   
                if(transform.position == waypoints[0].transform.position){bs.funds[1]+=200;} // if p1 passes by the start, +200 to p1's money

                if (waypointIndex % 10 == 0)
                {
                    transform.Rotate(0, 90, 0);
                }

                waypointIndex += 1;
            }
        }
        else
        {
            curindex = waypointIndex - 1;
            bs.arrivedOnCity(1, curindex % 40);
            moveAllowed = false;
        }
    }

    public void goToJail()
    {
        transform.position = waypoints[10].transform.position;
        waypointIndex = 11;
        curindex = 10;
        moveAllowed = false;
    }
}
