using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//simple script that is used for level selecting
public class LevelSelector : MonoBehaviour {

    public SceneFader fader;

    public Button[] levelButtons;

    void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++) // for each buttons repeat this.
        {
            if(i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }

    public void Select (string levelName)
    {
        fader.FadeTo(levelName);
    }
}
