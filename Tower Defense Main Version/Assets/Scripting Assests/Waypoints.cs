using UnityEngine;

//class used for storing/setting all the transforms/waypoints in array allowing them to be called in the enemy script.
public class Waypoints : MonoBehaviour {

    // make a duplicate of everything in here and make ai nt so they have to cohose between 2 ways

    public static Transform[] points; // Static variable holding all Waypoitns
    public static Transform[] points1; // Static variable holding all Waypoitns

    public int WaypointID;

    void Start()
    {

    }

    void Awake() // Find all waypoints and load into point array
    {
        if (WaypointID == 1)
        {
            points = new Transform[transform.childCount]; // creates ("X") amounts of spaces in the points array (bases on how many waypoints)

            for (int i = 0; i < points.Length; i++) // Goes through them all!
            {
                points[i] = transform.GetChild(i); // saves their location
                Debug.Log(i);
            }
        }

        if (WaypointID == 2)
        {
            points1 = new Transform[transform.childCount]; // creates ("X") amounts of spaces in the points array (bases on how many waypoints)

            for (int i = 0; i < points1.Length; i++) // Goes through them all!
            {
                points1[i] = transform.GetChild(i); // saves their location
                Debug.Log(i + "!");
            }
        }

    }

}
