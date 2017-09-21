using UnityEngine;

//class used for storing/setting all the transforms/waypoints in array allowing them to be called in the enemy script.
public class Waypoints : MonoBehaviour {

    public static Transform[] points; // Static variable holding all Waypoitns

    void Awake() // Find all waypoints and load into point array
    {
        points = new Transform[transform.childCount]; // creates ("X") amounts of spaces in the points array (bases on how many waypoints)

        for (int i = 0; i < points.Length; i++) // Goes through them all!
        {
            points[i] = transform.GetChild(i); // saves their location
        }
    }

}
