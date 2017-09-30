using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class used to creating turrets
// it contains the range/turnspeed/fire rate & cooldown per shot.
// it also contains the location of the rotation of the turret - the bullet it uses and the fire point.
// this script also makes the bullets it creates seek enemies and destroy after "X" amount of time.
// it also contains a damage variable to which is similar how to the bullet works
public class Turret : MonoBehaviour {

    private Transform target; // used for storing current target 
    private Enemy targetEnemy; 

    // This variables are generic overall variables that are used passively
    [Header("General")]
    public float range = 15f; //range of how far turrent can attack.
    private float damage = 0; //contains the damage this turret will do (this is differnt to the bullet damage)

    [Header("Use Bullets(Default)")]
    public bool useBullets; // used for storing if this damage type is being used

    public GameObject bulletPrefab; // the bullet this turret uses
    public float fireRate = 1f; // turret shotting speed
    private float fireCountdown = 0f; // turret cooldown

    [Header("Use Money")]
    public bool useMoney = false;

    public int moneyGeneration; // contains how much moenm

    [Header("Use selfAOE")]
    public bool useAoeTurret = false; // used to check if the aoe turret is enabled (Automatically set to false because the USE BULLETS is the default)

    public float aoeSelfDamage; // the damage this turret will do every few seconds
    public float aoeFireRate = 1f; // 1 shot per second
    public ParticleSystem AOEimpactEffect;
    private float aoeFireCountdown = 0f; // simple countdown per shot

    [Header("Use Laser")]
    public bool useLaser = false; // checker to see if to use laser.

    public int damageOverTime = 30; // damage variable for the dot the laser does.
    public float slowAmount = .5f; // slow speed - currenlt 50%

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity SetUp Fields")]
    public string enemyTag = "EnemyGround";
    public float turnSpeed = 10f; // turret turn speed
    public Transform partToRotate; // object that rotates
    public Transform firePoint;//location as to where the bullet insitates from

    public Transform rangeCircle; // controls the visual to show range


    // Use this for initialization
    void Start () {

        rangeCircle.localScale = new Vector3(range * 1.6f , rangeCircle.localScale.y, range * 1.6f); // sets the range to be equal to the range it has. 
		InvokeRepeating("UpdateTarget", 0f, 0.5f); // instantly search for a target, then checky every 0.5seconds
	}
	
    void UpdateTarget() // searches for new target/current target
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // array holding all enemies with the tag enemy
        float shortestDistance = Mathf.Infinity; // math.infinity used so the distance is infnite while no enemy is found
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // get distance from current turret to enemy

            if (distanceToEnemy < shortestDistance) // if distance is shorter then current shortest distance, change the shortest distance to the new one and set this enemy as closest new enemy
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) // we have found a enemy and it is within our range.
        {
            target = nearestEnemy.transform; // sets the target
            targetEnemy = nearestEnemy.GetComponent<Enemy>();  // sets the nearest enemy to enemy with the tag.
        } else
        {
            target = null; // if target is no longer in distance change it.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useMoney)
        {
            Money();
        }

        if (target == null) // checks to see if there is a target or not.
        {
            if (useAoeTurret) // if this turret is enabled and has no target, disable the aoe visual effect
            {
                AOEimpactEffect.Stop();
            }
            // if the target is null(no target) then check to see if the laser is on and if the linerender is on, if so disable it because of no target
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    impactEffect.Stop();
                    impactLight.enabled = false;
                    lineRenderer.enabled = false;
                }
            }

           
            return;
        }

        LockOnTarget(); // if the target is not empty lock onto the nearest turret.

        

        if (useLaser) // if laser is on, shoot laser.
        {
            Laser();

        }
        if (aoeFireCountdown <= 0f && useAoeTurret) // if countdown is done and this turret is enablbed allow it to do this
        {
            selfAoe(); // shoot the turret
            aoeFireCountdown = 1f / aoeFireRate; // reset the shooting cooldown
        }

        if (fireCountdown <= 0f && useBullets) // if ready to shoot, shoot.
        {
           Shoot();
           fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime; // updates the timer in real time
        aoeFireCountdown -= Time.deltaTime; // updates the timer in real time
    }

    void LockOnTarget()
    {
        //target lock-on
        Vector3 dir = target.position - transform.position; // figures out distance between target and ourselves
        Quaternion lookRotation = Quaternion.LookRotation(dir); // set its look destination to be the direction to target
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles; // converted to eulerangles & we want to smoothly rotate to new target over time.
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f); // rotate only around the Y axis
    }

    void Laser()// function used for the laser
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime); // damages the enemy.
        targetEnemy.Slow(slowAmount); // slows the enemy.

        if (!lineRenderer.enabled) // if linerender is disabled, re-enable it.
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        // all below here controls the direction/rotation/start position of the laser beamer
        lineRenderer.SetPosition(0, firePoint.position);//sets the inital position (start) to be the firepoint positoon
        lineRenderer.SetPosition(1, target.position);// sets the end position to be the 2nd location i.e the target.

        Vector3 dir = firePoint.position - target.position; // makes a rotation that points back to our turret.

        impactEffect.transform.position = target.position + dir.normalized; // moves the axis by 1, causing it to be 0.5 out.

        impactEffect.transform.rotation = Quaternion.LookRotation(dir); // makes the impact effect rotate towards the turret location.

    }
    // method used for the self aoe turret
    // it collects all targets within the range of the turret
    // then allows them to be damaged
    void selfAoe() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range); // collects all colliders hit inside this explosion radius and stores them in the collider array.

        foreach (Collider collider in colliders) // loop through all that list and each enemy hit with enemy tag, deal damage.
        {
            if (collider.tag == "EnemyGround") // if enemy has the tag enemyflying allow it to be damage.
            {
                damage = aoeSelfDamage;
                AOEimpactEffect.Play();
                Damage(collider.transform);// damage the enemy
            }
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null) // if there's a bullet seek a target
        {
            bullet.Seek(target);
        }
    }

    public void Money() // controls the money gaining
    {
            PlayerStats.moneyGenerationAmount += moneyGeneration; // increases the money gain per second by the towers set amount
            useMoney = false; // disables it after upgraded so it only runs once.
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        if (e != null) // if there's a enemy with tag enemy, then deal damage.
        {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected() // draws the circles (attack range) of turrets
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    
}
