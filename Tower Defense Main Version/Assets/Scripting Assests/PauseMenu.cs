using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//used to control if the game is paused or not
public class PauseMenu : MonoBehaviour {

    public GameObject ui;

    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";

    private void Update()
    {
        // checks to see if one of the following pause buttons have been called.
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf); // flips between on or off

        if (ui.activeSelf) // if the toggle is on
        {
            Time.timeScale = 0f; // stops time.

        }
        else
        {
            Time.timeScale = 1f; // restart time.
        }
    }

    //reloads the level
    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name); // don't forget to put scenefader into unity and in the inspector everytime.
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuSceneName);
    }
}
