using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//controls the play/quit functions on the main menu.
public class MainMenu : MonoBehaviour
{
    public string levelToload = "World_0";

    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(levelToload);
    }

    public void Quit()
    {
        Debug.Log("Exciting!");
        Application.Quit();
    }

}