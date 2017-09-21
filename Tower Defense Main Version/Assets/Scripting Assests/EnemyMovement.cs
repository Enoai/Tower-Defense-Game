using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] // wont work unless we have a type enemy on the object/
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int wavePointIndex = 0; // current waypoint persusing, i.e moving 1-2-3-4... ETC

    private Enemy enemy; // creates a private link to the enemy class.

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0]; // set the first target to be the first waypoint
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; // figures out that location to go to.
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World); // makes sure it always has a fixed speed.

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) // If enemy is within a set proximity of next waypoint, set next waypoint
        {
            GetNextWaypoint(); // Calls the next waypoint
        }

        enemy.speed = enemy.startSpeed; // resets the speed, thus allowing the object to go back to it's normal speed again if not slowed again.
    }


    void GetNextWaypoint() // fiugres out the next wayoupoint to go to and sets it.
    {
        if (wavePointIndex >= Waypoints.points.Length - 1) // kill enemy reaching end
        {
            EndPath();
            return; // stops the code going down to next area before running this code.
        }

        wavePointIndex++; // move array value up by one
        target = Waypoints.points[wavePointIndex]; // sets the new target
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
