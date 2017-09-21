using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RoundsSurvived : MonoBehaviour {

    public Text roundsText;

    void OnEnable() // when script is loaded, run this.
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText()
    {
        roundsText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f); // waits over half a second so text can load in

        while (round < PlayerStats.Rounds) // counts up to the amount of rounds done in a cool way.
        {
            round++; // slowly increases the level account
            roundsText.text = round.ToString();

            yield return new WaitForSeconds(.05f); // waits a milisecond before changing text to the next one
        }
    }
}
