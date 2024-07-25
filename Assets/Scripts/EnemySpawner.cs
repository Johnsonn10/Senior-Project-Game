using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;

    [SerializeField] private float countdown;   //change to num of enemies later
    [SerializeField] private GameObject spawnPoint;

    private int currentWave = 0;
    private void Start()
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].numEnemies = waves[i].enemies.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = waves[currentWave].timeToNextWave;
            StartCoroutine(SpawnWave());
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < waves[currentWave].enemies.Length; i++)
        {
            Enemy e = Instantiate(waves[currentWave].enemies[i], spawnPoint.transform);

            e.transform.position = spawnPoint.transform.position;

            yield return new WaitForSeconds(waves[currentWave].timeToSpawn);
        }
    }

    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public float timeToNextWave;
        public float timeToSpawn;

        [HideInInspector] public int numEnemies;
    }
}
