using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public int enemySpawnCountMin = 3;
    public int enemySpawnCountMax = 5;

    private float enemySpawnXMin = -15.0f;
    private float enemySpawnXMax = 15.0f;
    private float enemySpawnYMin = -3.0f;
    private float enemySpawnYMax = 5.0f;

    public GameObject enemyPrefab;
    public Transform enemyParent;
    public Transform playerTransform;

    private float timeSinceStart;

    private ArrayList enemiesStored;

    private Vector2 enemySize;

    private Boolean alreadySpawned;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceStart = 0.0f;

        enemiesStored = new ArrayList();

        enemySize = new Vector2(0.7f, 0.7f);

        alreadySpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > 3 && !alreadySpawned)
        {
            SpawnEnemies();
            alreadySpawned = true;
        }
    }

    void SpawnEnemies()
    {
        int enemyCount = UnityEngine.Random.Range(enemySpawnCountMin, enemySpawnCountMax);

        int spawnedCount = 0;

        int spawnAttempts = 0;

        while (spawnedCount < enemyCount)
        {
            float spawnX = UnityEngine.Random.Range(enemySpawnXMin, enemySpawnXMax);
            float spawnY = UnityEngine.Random.Range(enemySpawnYMin, enemySpawnYMax);

            Vector2 spawnLocation = new Vector2(spawnX, spawnY);

            UnityEngine.Debug.Log(spawnX + " " + spawnY);

            spawnAttempts++;

            if (Physics2D.OverlapBox(spawnLocation, enemySize, 0.0f) == null)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity, enemyParent);

                spawnedEnemy.GetComponent<Enemy>().playerTransform = playerTransform;

                spawnedEnemy.SetActive(true);

                enemiesStored.Add(spawnedEnemy);
                spawnedCount++;
                spawnAttempts = 0;
            }

            if (spawnAttempts > 99)
            {
                spawnedCount++;
            }
        }
    }
}
