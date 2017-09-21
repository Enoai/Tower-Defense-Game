using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//similar to the Lives ui, to which constantly updated the money ui on the screen with the correct amount of money.
public class MoneyUI : MonoBehaviour {

    public Text moneyText;
	// Update is called once per frame
	void Update () {

        moneyText.text = "$" + PlayerStats.Money.ToString(); // constantly update the money text with the current money from palyerstats

	}
}
