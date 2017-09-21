using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//simple script to whichs just contains a few variables to which are used in other classes (build manager etc)
[System.Serializable]
public class TurretBlueprint
{
    //Maybe attach a range indicator to a turret blueprint as a seperate object.

    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;


    public int GetSellAmount() // returns sell cost.
    {
        return cost / 2;
    }

    public int GetUpgradedSellAmount()
    {
        return cost + upgradeCost / 2;
    }

}