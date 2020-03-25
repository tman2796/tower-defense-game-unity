using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Bullet))]
public class Turret : MonoBehaviour {

    private Transform target;
    private EnemyController enemy;

    [Header("General")] //Attributes of Turret

    public float range = 15f;
    public int damageOverTime = 5;

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")] //To not be changed by the user

    public string enemyTag = "Enemy";

    public Transform rotateTurret;
    public float turnSpeed = 10;
    public Transform firePoint;
    Bullet bullet;



    // Use this for initialization
    void Start () {
        bullet = GetComponent<Bullet>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
		
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Temp variables to hold shortest distances to nearest enemies
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            // loops through enemies that come into range to attack the closest enemy
            //finds the distance per enemy within range
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            //if distanceToEnemy is less than shortestDistance then set the shortest distance to the distance to enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else if (distanceToEnemy > range)
            {
                Destroy(bullet);
            }
        }

        //makes sure enemy within range is not null and is within the range of the turret
        if (nearestEnemy != null && shortestDistance <= range)
        {
            //sets target to the enemy within ranges position

            target = nearestEnemy.transform;
        }
        //if no targets within range then nothing to target
        else if(shortestDistance >= range)
        {
            Destroy(bullet);
            target = null;
        }
    }

    // Update is called once per frame
    void Update () {

        if (target == null)
        {
            return;
        }
        //gets direction of rotation to the direction of the enemy from the turret
        //Target lock on
        LockOnTarget();

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
	}

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotateTurret.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        rotateTurret.rotation = Quaternion.Euler(0f, 0f, rotation.z);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Hunt(target);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
