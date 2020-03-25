using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Turret))]
public class Bullet : MonoBehaviour {

    private Transform target;

    //private GameObject targetDistance;

    //Turret turret;

    public float speed = 50f;

    public int damage = 20;

    public GameObject impactEffect;

    public string enemyTag = "Enemy";

    private Turret turret;

    public void Hunt(Transform _target)
    {
        target = _target;
    }
	
	// Update is called once per frame
	void Update () {

        //float distanceToEnemy = Vector3.Distance(transform.position, targetDistance.transform.position);
        //if (target == null || distanceToEnemy >= turret.range)
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
	}

    public void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Damage(target);
        Destroy(effectInstance, 2f);
        //Destroy(target.gameObject);
        //Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {

        EnemyController e = enemy.GetComponent<EnemyController>();

        if (e != null)
        {
            e.EnemyTakeDamage(damage);
        }
    }
}
