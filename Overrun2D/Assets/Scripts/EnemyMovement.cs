using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int waveIndex = 0;
    private SpriteRenderer zombieSprite;
    private EnemyController enemy;


    void Start()
    {
        enemy = GetComponent<EnemyController>();
        zombieSprite = GetComponent<SpriteRenderer>();
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.startSpeed * Time.deltaTime, Space.World);

        //check direction of normalized for movinig left right or down normalized.y... and so on

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waveIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waveIndex++;
        target = Waypoints.points[waveIndex];
        if (waveIndex == 2 || waveIndex == 6 || waveIndex == 10 || waveIndex == 14)
        {
            zombieSprite.flipX = true;
        }
        else if (waveIndex == 0 || waveIndex == 4 || waveIndex == 8 || waveIndex == 12)
        {
            zombieSprite.flipX = false;
        }
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

}
