using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script used for controlling if the game is over.
public class GameManager : MonoBehaviour {

    public static bool GameIsOver;

    public GameObject gameOverUI; // holds our game over ui
    public GameObject completeLevelUI;

    void Start()
    {
        GameIsOver = false; // because of static varaible, we set the game is over back to it's default of false.
    }

	// Update is called once per frame
	void Update () {
        if (GameIsOver) // if the game is ended, don't let it repeat.
        {
            return;
        }

		if (PlayerStats.Lives <= 0) // if player lives is 0, initate end game
        {
            EndGame();
        }
	}

    void EndGame() //Runs when game is over.
    {
        GameIsOver = true; // sets variable to true to stop scripts from running.

        gameOverUI.SetActive(true); // enables the UI upon game being over.

    }

    public void WinLevel() // upon winning the level show the win level screen.
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }
}
