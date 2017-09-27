 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;

//class used for storing/setting all the transforms/waypoints in array allowing them to be called in the enemy script.
public class Waypoints : MonoBehaviour {

    // make a duplicate of everything in here and make ai nt so they have to cohose between 2 ways

    public Transform[] wayPointsGround;
    public Transform[] wayPointsAir;

    public GameObject[] spawnPoints; // contains all the current spawnPoints on the map
    public GameObject currentSpawnPoint; // contains the currently selected spawnpoint
    public int spawnPointIndex;// contains the number

    private int wayPointAirNumber = 0;
    private int wayPointsGroundNumber = 0;

    private int wayPointAirCounter = 0;
    private int wayPointsGroundCounter = 0;

    void Awake() // Find all waypoints and load into point array
    {
        // chooses randomly a spawn point from the list
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint"); // Gathers all spawnpoitns on the map with the tag of this
        spawnPointIndex = Random.Range(0, spawnPoints.Length);
        currentSpawnPoint = spawnPoints[spawnPointIndex];
        print(currentSpawnPoint.name);

        // For loop used to count up how many children are in the gameobject then figure out the amount of air or ground waypoints and create the correct number
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.tag == "GroundWaypoint")
            {
                wayPointsGroundCounter++;
            }
            if (child.tag == "FlyingWaypoint")
            {
                wayPointAirCounter++;
            }
        }

        // the number genereated from the previous for loop is used here to correctly create the right amount of places required for the transofrms,
        // if this is not done it would make the (END PATH) Method be disfunctional and be unable ot be called because of being too large ()
            wayPointsGround = new Transform[wayPointsGroundCounter];
            wayPointsAir = new Transform[wayPointAirCounter];

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
