using System.Collections.Generic;
using UnityEngine;

// script used for controlling bullet damage-speed-explosion radius and impact effects
public class Bullet : MonoBehaviour {

    private Transform target;

    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;

    public int damage = 50;

    public void Seek (Transform _target)
    {
        target = _target; // sets target to the new target variable
    }


	// Update is called once per frame
	void Update () {

		if (target == null)// if the target is null destroy the bullet
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame) // checks to see if we hit something.
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World); // havent yet hit target and need to move.
        transform.LookAt(target); // makes the bullet look at the designtaed target.
	}

    void HitTarget()
    {
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation); // create bullet hit effect - also cast the obejct into a gameobject.
        Destroy(effectIns, 5f); //destroy explosion/hit effect after 5 seconds

        if (explosionRadius > 0f) // if explosion radius is bigger then 0 do this
        {
            Explode();
        }
        else // else just damage it normally
        {
            Damage(target);
        }

        Destroy(gameObject);// then destroy obect
    }

    void Explode()
    {
       Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); // collects all colliders hit inside this explosion radius and stores them in the collider array.

        foreach (Collider collider in colliders) // loop through all that list and each enemy hit with enemy tag, deal damage.
        {
            if (collider.tag == "EnemyFlying") // if enemy has the tag enemyflying allow it to be damage.
            {
                Damage(collider.transform);// damage the enemy
                
            }
        }
    }

    void Damage (Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null) // if there's a enemy with tag enemy, then deal damage.
        {
            e.TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected() // draws a circle to show explosion radius of missle.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
