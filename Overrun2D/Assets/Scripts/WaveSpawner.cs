using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform normalZombie;
    public Transform heavyZombie;
    public Transform speedZombie;
    public Transform spawnPoint;

    public float countDownTimer = 10f;
    private float countdown = 2f;

    public Text waveCountdownText;

    private int waveIndex = 0;

    public Text waveDisplay;

    public int waveNumber = 0;


    void Start()
    {
        waveDisplay.text = "Wave: " + waveNumber;
    }
    public void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = countDownTimer;
            waveDisplay.text = "Wave: " + waveNumber;
            waveNumber++;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    //Doesn't spawn enemies on top of each other
    //Spawns then in 0.8f time increments at a time when the new wave spawns.
    IEnumerator SpawnWave()
    {

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnNormalZombie();
            yield return new WaitForSeconds(1.5f);
        }
        if (waveIndex == 10 || waveIndex == 15 || waveIndex == 20 || waveIndex == 25 || waveIndex == 30)
        {
            for (int i = 0; i < 5;) { 
                SpawnHeavyZombie();
                yield return new WaitForSeconds(2f);
                i++;
            }
        }
        if (waveIndex == 10 || waveIndex == 15 || waveIndex == 20 || waveIndex == 25 || waveIndex == 30)
        {
            for (int i = 0; i < 10;)
            {
                SpawnSpeedZombie();
                yield return new WaitForSeconds(3f);
                i++;
            }
        }
        waveIndex++;
        PlayerStats.rounds++;
    }

    void SpawnNormalZombie()
    {
        Instantiate(normalZombie, spawnPoint.position, spawnPoint.rotation);
    }
    void SpawnHeavyZombie()
    {
        Instantiate(heavyZombie, spawnPoint.position, spawnPoint.rotation);
    }
    void SpawnSpeedZombie()
    {
        Instantiate(speedZombie, spawnPoint.position, spawnPoint.rotation);
    }

}
