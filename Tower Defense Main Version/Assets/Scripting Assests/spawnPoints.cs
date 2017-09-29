using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoints : MonoBehaviour
{
    // script used for controlling the spawn locations of flying and ground
    // allowing it to be randomized
    [Header("Flying SpawnPoint Information")]
    public Transform[] spawnLocationsFlying; // contains all the current spawnPoints on the map
    public static Transform currentSpawnPointFlying; // contains the currently selected spawnpoint
    private int spawnPointIndexFlying;// contains the number
    private int spawnPointNumberFlying = 0;
    private int spawnPointCounterFlying = 0;

    [Header("Ground SpawnPoint Information")]
    public Transform[] spawnLocationsGround; // contains all the current spawnPoints on the map
    public static Transform currentSpawnPointGround; // contains the currently selected spawnpoint
    private int spawnPointIndexGround;// contains the number
    private int spawnPointNumberGround = 0;
    private int spawnPointCounterGround = 0;


    void Awake() //Finds all the spawns points the spawnpoint holder, once found counts how many of each.
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.tag == "SpawnPointFlying")
            {
                spawnPointCounterFlying++;
            }
            if (child.tag == "SpawnPointGround")
            {
                spawnPointCounterGround++;
            }
        }

        //creates teh correct amount of array holders due to the previous method
        spawnLocationsGround = new Transform[spawnPointCounterGround];
        spawnLocationsFlying = new Transform[spawnPointCounterFlying];

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child.tag == "SpawnPointGround")
            {
                spawnLocationsGround[spawnPointNumberGround] = transform.GetChild(i);
                spawnPointNumberGround++;
            }
            if (child.tag == "SpawnPointFlying")
            {
                spawnLocationsFlying[spawnPointNumberFlying] = transform.GetChild(i);
                spawnPointNumberFlying++;
            }
        }

        switchSpawnPoint();
    }

    // changed all the floor and air current spawnpoints to something else
    public void switchSpawnPoint()
    {
        spawnPointIndexGround = Random.Range(0, spawnLocationsGround.Length);
        currentSpawnPointGround = spawnLocationsGround[spawnPointIndexGround];
        print(currentSpawnPointGround.name);

        spawnPointIndexFlying = Random.Range(0, spawnLocationsFlying.Length);
        currentSpawnPointFlying = spawnLocationsFlying[spawnPointIndexFlying];
        print(currentSpawnPointFlying.name);
    }
} 