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


    //references for gameobjects and transforms
    public GameObject enemyGroundPrefab;
    public Transform enemyGroundParent;

    public GameObject enemyFloatPrefab;
    public Transform enemyFloatParent;

    public GameObject projectilePrefab;
    public Transform projectileParent;

    public Transform playerTransform;


    private float timeSinceStart;

    private ArrayList enemiesStored;

    private Vector2 enemyGroundSize;
    private Vector2 enemyFloatSize;


    private float spawnCooldown;

    private float randomCooldownTimeMin = 3.0f;
    private float randomCooldownTimeVariance = 2.0f;


    private float ground2FloatRatio = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceStart = 0.0f;

        enemiesStored = new ArrayList();

        enemyGroundSize = new Vector2(0.7f, 0.7f);
        enemyFloatSize = new Vector2(0.5f, 0.5f);

        spawnCooldown = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceStart += Time.deltaTime;

        float randomTime = randomCooldownTimeMin + UnityEngine.Random.Range(0.0f, randomCooldownTimeVariance);
        //spawnEnemiesOneByOne
        spawnCooldown += Time.deltaTime;
        if (spawnCooldown > randomTime)
        {
            SpawnEnemies(2);
            spawnCooldown = 0.0f;
            randomTime = randomCooldownTimeMin + UnityEngine.Random.Range(0.0f, randomCooldownTimeVariance);
        }
    }

    void SpawnEnemies(int count)
    {
        int spawnedCount = 0;

        int spawnAttempts = 0;

        while (spawnedCount < count)
        {
            spawnAttempts++;

            float spawnX = UnityEngine.Random.Range(enemySpawnXMin, enemySpawnXMax);
            float spawnY = UnityEngine.Random.Range(enemySpawnYMin, enemySpawnYMax);

            Vector2 spawnLocation = new Vector2(spawnX, spawnY);

            float ratio = UnityEngine.Random.Range(0.0f, 1.0f);

            UnityEngine.Debug.Log(ratio);

            Vector2 enemySize;

            Boolean ratioSize = true;

            if (ratio < ground2FloatRatio)
            {
                enemySize = enemyGroundSize;
            }
            else
            {
                enemySize = enemyFloatSize;
                ratioSize = false;
            }

            if (Physics2D.OverlapBox(spawnLocation, enemySize, 0.0f) == null)
            {
                if (ratioSize)
                {
                    SpawnEnemyGround(spawnLocation);
                }
                else
                {
                    SpawnEnemyFloat(spawnLocation);
                }
                spawnedCount++;
            }

            if (spawnAttempts > 99)
            {
                spawnedCount++;
            }
        }
    }

    void SpawnEnemyGround(Vector2 spawnLocation)
    {
        GameObject spawnedEnemy = Instantiate(enemyGroundPrefab, spawnLocation, Quaternion.identity, enemyGroundParent);
        spawnedEnemy.GetComponent<EnemyGround>().playerTransform = playerTransform;
        spawnedEnemy.SetActive(true);
        enemiesStored.Add(spawnedEnemy);
    }

    void SpawnEnemyFloat(Vector2 spawnLocation)
    {
        GameObject spawnedEnemy = Instantiate(enemyFloatPrefab, spawnLocation, Quaternion.identity, enemyFloatParent);
        spawnedEnemy.GetComponent<EnemyFloat>().playerTransform = playerTransform;
        spawnedEnemy.GetComponent<EnemyFloat>().projectilePrefab = projectilePrefab;
        spawnedEnemy.GetComponent<EnemyFloat>().projectileParent = projectileParent;
        spawnedEnemy.SetActive(true);
        enemiesStored.Add(spawnedEnemy);
    }
}
