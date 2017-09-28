using System.Collections.Generic;
using UnityEngine;
using System.Collections; // used for coruntines
using UnityEngine.UI;

//script used to spawn enemies.
// it controls the spawn between waves and also what the wave spawner spawns.
public class WaveSpawner : MonoBehaviour { // Script used to spawn monsters in waves.

    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public float timeBetweenWaves = 5f; //variable used to space spawn between monsters
    private float countdown = 2f; // timer between spawning first wave

    public Text waveCountdownText;

    public GameManager gameManger;
    public spawnPoints spawnPoints;

    private int waveIndex = 0; // amount of npcs spawnt per wave

    void Start()
    {
        EnemiesAlive = 0;
    }

    void Update()
    {
        if (EnemiesAlive > 0) // if there's enemies alive don't start next wave.
        {
            return;
        }

        if (waveIndex == waves.Length) // if they've beaten the level do this.
        {
            if (PlayerStats.Lives > 0) // stops the game allowing you to win if the last enemy is killed by the game over.
            {
                gameManger.WinLevel();
                this.enabled = false; // disables the script
            }

            if(PlayerStats.Lives <= 0)
            {
                return;
            }
        }

        if (countdown <= 0f) // If the countdown is ready, spawn the next wave!
        {
            StartCoroutine(SpawnWave()); // begin the coruntine for spawningwaves
            countdown = timeBetweenWaves; // set countdown to now be the timeinbetweenwaves.
            return;
        }

        countdown -= Time.deltaTime; // makes it countdown in real time.

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // causes it to never go below 0

        waveCountdownText.text = string.Format("{0:00.00}", countdown); // formats the count down to use the format seen on the left (0:00.00)
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++; // incrases the current play round level by 1, show casing what level they are on currnetly.

        Wave wave = waves[waveIndex]; // if it's the first wave take the first wave element === etc.

        EnemiesAlive = wave.count1 + wave.count2 + wave.count3 + wave.count4 + wave.count5; // sets the amount of enemies alive qual to all eneimes in arrays.

        // goes through all arrays of enemies
        for (int i = 0; i < wave.count1; i++)
        {
            SpawnEnemy(wave.enemy1);
            yield return new WaitForSeconds(1f / wave.rate1); // wait for half a second
        }
        spawnPoints.switchSpawnPoint(); // After everywave change the spawnpoint to make the game more random

        for (int i = 0; i < wave.count2; i++)
        {
            SpawnEnemy(wave.enemy2);
            yield return new WaitForSeconds(1f / wave.rate2); // wait for half a second
        }
        spawnPoints.switchSpawnPoint(); // After everywave change the spawnpoint to make the game more random

        for (int i = 0; i < wave.count3; i++)
        {
            SpawnEnemy(wave.enemy3);
            yield return new WaitForSeconds(1f / wave.rate3); // wait for half a second
        }
        spawnPoints.switchSpawnPoint(); // After everywave change the spawnpoint to make the game more random

        for (int i = 0; i < wave.count4; i++)
        {
            SpawnEnemy(wave.enemy4);
            yield return new WaitForSeconds(1f / wave.rate4); // wait for half a second
        }
        spawnPoints.switchSpawnPoint(); // After everywave change the spawnpoint to make the game more random

        for (int i = 0; i < wave.count5; i++)
        {
            SpawnEnemy(wave.enemy5);
            yield return new WaitForSeconds(1f / wave.rate5); // wait for half a second
        }
        spawnPoints.switchSpawnPoint(); // After everywave change the spawnpoint to make the game more random

        waveIndex++; // Increase the waveIndex


    }

    void SpawnEnemy(GameObject enemy) // spawn the enmies
    {
        // checks the tags and sets a random spawn point based on what type of enemy they are
        if (enemy.tag == "EnemyFlying")
        {
            Instantiate(enemy, spawnPoints.currentSpawnPointFlying.position, spawnPoints.currentSpawnPointFlying.rotation); // spawn in this specfic prefab at the spawnpoint location and rotation
        }
        else
        {
            Instantiate(enemy, spawnPoints.currentSpawnPointGround.position, spawnPoints.currentSpawnPointGround.rotation); // spawn in this specfic prefab at the spawnpoint location and rotation
        }      
    }
}
