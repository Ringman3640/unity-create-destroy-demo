using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSystem : MonoBehaviour
{
    // Singleton EggSystem instance
    private static EggSystem instance;

    // Egg object reference for spawning instantiation
    private GameObject eggPrefab;

    // Count of all eggs currently in the scene
    private int count;

    // Get singleton instance
    public static EggSystem Instance
    {
        get { return instance; }
    }

    // Get current egg count
    public int EggCount
    {
        get { return count; }
    }

    // Awake is called on script initialization
    void Awake()
    {
        eggPrefab = Resources.Load<GameObject>("Prefabs/Egg");

        if (instance == null)
        {
            instance = this;
        }
    }

    // Spawn an Egg at a given position and starting angle
    public void SpawnEgg(Vector3 position, Vector3 direction)
    {
        GameObject eggInstance = Instantiate(eggPrefab);
        eggInstance.transform.position = position;
        eggInstance.transform.up = direction;
    }

    // Increment the scene Egg count
    public void IncrementCount()
    {
        ++count;
    }

    // Decrement the scene Egg count
    public void DecrementCount()
    {
        if (count > 0)
        {
            --count;
        }
    }
}
