using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// used for game over functions
public class GameOver : MonoBehaviour { 

    public SceneFader sceneFader;

    public string menuSceneName = "MainMenu";

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Debug.Log("Go To Menu");
        sceneFader.FadeTo(menuSceneName);
    }

}
