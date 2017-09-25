using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class used to store the players current stats lives/money
// it also sets the base stats back to a default, because of a static variable (meaning ifa player reopended this app, it would be set back to the orignal amount before they left, thus why the base int is in place)
public class PlayerStats : MonoBehaviour {

    public static int Money; // variable accessible throughout the entire world
    public static int moneyGenerationAmount;
    public int startMoney = 400; // because of the orignal static variable it makes the money go back to start money upon restarting the game.

    // same as money.
    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    void Start ()
    {
        Money = startMoney;
        Lives = startLives;
        moneyGenerationAmount = 5;
        Rounds = 0;
    }

    void Awake()
    {
        StartCoroutine(moneyBuffer());
    }

    IEnumerator moneyBuffer()
    {
        while (true)
        {
        yield return new WaitForSeconds(5f);
        Money = moneyGenerationAmount + Money;
        }       
    }
}
