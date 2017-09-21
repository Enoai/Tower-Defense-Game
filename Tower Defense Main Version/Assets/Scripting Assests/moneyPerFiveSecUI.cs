using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moneyPerFiveSecUI : MonoBehaviour {

    public Text moneyPerFiveText;
    // Updates the visual for showing how much money is gained per 5 seconds
    void Update()
    {
        moneyPerFiveText.text = "$" + PlayerStats.moneyGenerationAmount.ToString() + " Per 5s"; // constantly update the money text with the current money from palyerstats
    }
}
