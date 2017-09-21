using UnityEngine;

// Normal controller to which allows the user to move with directional arrows and also being near the corner of a creen.
public class CameraController : MonoBehaviour {


    public float panSpeed = 30f; // used to controll scrolling around screen. speed
    public float panBorderThickness = 10f; // length away from location (BORDER THICKNESS)

    // Controlls the disatnce/speed of the cameras
    public float scrollSpeed = 5f;

    public float minX = 10;
    public float maxX = 80;

    public float minZ = 10;
    public float maxZ = 80;

    public float minY = 10;
    public float maxY = 80;


    // Update is called once per frame
    void Update ()
    {
        if (GameManager.GameIsOver) // disables camera movement when the game is over
        {
            this.enabled = false; // disables camera controller
            return;
        }

        // Camera Controller
		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); // moves forward the camera by using the world space's coridnates (Space.world uses the world coridnates not the object)
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); // moves forward the camera by using the world space's coridnates (Space.world uses the world coridnates not the object)
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); // moves forward the camera by using the world space's coridnates (Space.world uses the world coridnates not the object)
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World); // moves forward the camera by using the world space's coridnates (Space.world uses the world coridnates not the object)
        }


        // method used for controlling zooming in and out
        float scroll =  Input.GetAxis("Mouse ScrollWheel"); // gets the unity function for scroll wheel
        Vector3 pos = transform.position; // shortcut variable for transformpostion

        pos.x = Mathf.Clamp(pos.x, minX, maxX); //clamps a value

        pos.z = Mathf.Clamp(pos.z, minZ, maxZ); //clamps a value

        pos.y -= scroll * 500 * scrollSpeed * Time.deltaTime; // zooms in and out *1000 used for easier numbers and scroll wheels makes tiny numbers
        pos.y = Mathf.Clamp(pos.y, minY, maxY); //clamps a value

        transform.position = pos; // sets the transform change to the value





    }
}
