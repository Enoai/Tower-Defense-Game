using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))] // wont work unless we have a type enemy on the object/
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int wavePointIndex = 0; // current waypoint persusing, i.e moving 1-2-3-4... ETC

    private Enemy enemy; // creates a private link to the enemy class.

    public Waypoints waypoints;

    
    void Start()
    {
        waypoints = FindObjectOfType<Waypoints>(); // instantlly assigns waypoints to all prefabs.
        enemy = GetComponent<Enemy>();

        if (enemy.tag == "EnemyGround")
        {
            target = waypoints.wayPointsGround[0];
        }
        else
        {
            target = waypoints.wayPointsAir[0];
        }
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

        if (enemy.tag == "EnemyGround")
        {
            if (wavePointIndex >= waypoints.wayPointsGround.Length - 1) // kill enemy reaching end
            {
                EndPath();
                return; // stops the code going down to next area before running this code.
            }
        }
        else
        {
            if (wavePointIndex >= waypoints.wayPointsAir.Length - 1) // kill enemy reaching end
            {
                EndPath();
                return; // stops the code going down to next area before running this code.
            }
        }

        wavePointIndex++; // move array value up by one

        if (enemy.tag == "EnemyGround")
        {
            target = waypoints.wayPointsGround[wavePointIndex]; // sets the new target
        }
        else
        {
            target = waypoints.wayPointsAir[wavePointIndex]; // sets the new target
        }

    }

    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
