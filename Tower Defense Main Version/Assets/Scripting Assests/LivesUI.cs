using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//script used for updating the lives text on the gui with the current lives.
public class LivesUI : MonoBehaviour {

    public Text livesText;
	
	// Update is called once per frame
	void Update () {
        livesText.text = PlayerStats.Lives.ToString() + " LIVES"; // sets the lives to a string with the lives text at the end.
	}
}
