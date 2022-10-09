using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // Singleton instance
    private static EnemySystem instance;

    // Egg object reference for spawning instantiation
    private GameObject enemyPrefab;

    // The target amount of enemies to appear at once on screen
    public int targetEnemyCount = 10;

    // Count of all Enemies on the scene
    private int count = 0;

    // Count of all Enemies destroyed
    private int destroyed = 0;
    private int touchDestroyed = 0;

    // Range in which Enemies can spawn within the world boundaries
    // Min: 0.2f
    // Max: 1.0f
    public float spawnRange = 0.9f;
    private const float MIN_RANGE = 0.2f;
    private const float MAX_RANGE = 1.0f;

    // Get the singleton instance
    public static EnemySystem Instance
    {
        get { return instance; }
    }

    // Get the current enemy count
    public int EnemyCount
    {
        get { return count; }
    }

    // Get destroyed count
    public int DestroyedCount
    {
        get { return destroyed; }
    }

    // Get touched destroyed count
    public int TouchDestroyedCount
    {
        get { return touchDestroyed; }
    }

    // Awake is called on script initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Get Enemy prefab reference
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");

        // Check spawnRange
        if (spawnRange < MIN_RANGE)
        {
            spawnRange = MIN_RANGE;
        }
        else if (spawnRange > MAX_RANGE)
        {
            spawnRange = MAX_RANGE;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AddEnemies();
    }

    // Decrement the enemy counter
    public void DecrementSpawnCount()
    {
        if (count > 0)
        {
            --count;
        }
    }

    public void IncrementDestroyedCount(bool destroyedByPlayerTouch)
    {
        ++destroyed;
        if (destroyedByPlayerTouch)
        {
            ++touchDestroyed;
        }
    }

    // Add Enemies to the scene until target enemy count is reached
    private void AddEnemies()
    {
        while (count < targetEnemyCount)
        {
            // Get random position in bounds
            Vector3 pos = new();
            pos.x = Random.Range(CameraSystem.Bounds.min.x, CameraSystem.Bounds.max.x) * spawnRange;
            pos.y = Random.Range(CameraSystem.Bounds.min.y, CameraSystem.Bounds.max.y) * spawnRange;
            pos.z = 0f;

            // Spawn Enemy
            GameObject enemyInstance = Instantiate(enemyPrefab);
            enemyInstance.transform.position = pos;

            ++count;
        }
    }
}
