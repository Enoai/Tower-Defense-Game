 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;

//class used for storing/setting all the transforms/waypoints in array allowing them to be called in the enemy script.
public class Waypoints : MonoBehaviour {

    // make a duplicate of everything in here and make ai nt so they have to cohose between 2 ways

    public Transform[] wayPointsGround;
    public Transform[] wayPointsAir;

    private int wayPointAirNumber = 0;
    private int wayPointsGroundNumber = 0;

    void Awake() // Find all waypoints and load into point array
    {
        wayPointsGround = new Transform[transform.childCount];
        wayPointsAir = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.tag == "GroundWaypoint")
            {
                wayPointsGround[wayPointsGroundNumber] = transform.GetChild(i);
                wayPointsGroundNumber++;
            }
            if (child.tag == "FlyingWaypoint")
            {
                wayPointsAir[wayPointAirNumber] = transform.GetChild(i);
                wayPointAirNumber++;
            }
        }
    }
}
