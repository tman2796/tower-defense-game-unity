using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float startSpeed = 5f;
    private float speed;
    int normalZombieValue = 1;
    int heavyZombieValue = 30;
    int speedZombieValue = 15;
    public float startHealth = 100f;
    private float health;
    public GameObject deathEffect;

    [Header("Unity Stuff")]

    public Image healthBar;
    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void EnemyTakeDamage(int amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            //if (health == 500)
            //{
            NormalZombieDie();
            HeavyZombieDie();
            SpeedZombieDie();
           // }
            //if (health == 100)
           // {
            //    SpeedZombieDie();
           // }
            //if (health == 2000)
            //{
            //    HeavyZombieDie();
            //}
        }
    }

    private void HeavyZombieDie()
    {
        PlayerStats.Money += heavyZombieValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 6f);

        Destroy(gameObject);
    }
    private void SpeedZombieDie()
    {
        PlayerStats.Money += speedZombieValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        Destroy(gameObject);
    }
    private void NormalZombieDie()
    {
        PlayerStats.Money += normalZombieValue;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 4f);

        Destroy(gameObject);
    }
}
