using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteLevel : MonoBehaviour {

    public SceneFader sceneFader;

    public string nextLevel = "Level02"; // must be set on each game manager. // used for changing levels to next one
    public int levelToUnlock = 2; // controls the level on front page unlocked

    public string menuSceneName = "MainMenu";

    void OnEnable()
    {
        if (levelToUnlock > PlayerPrefs.GetInt("levelReached", 1)) // if the current level has a higher level to unlock then playerprefs, replace it.
        {
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }

    public void Continue()
    {
        Debug.Log("LEVEL WON!");
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }


}
