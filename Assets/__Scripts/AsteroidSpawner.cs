#define DEBUG_Spawner
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidSpawner : MonoBehaviour
{
    [Header("Initialize with the Asteroid Scriptable Object")]
    [SerializeField]
    private AsteroidsSO AsteroidsSO_Instance;

    private GameObject[] Asteroids;

    // Start is called before the first frame update
    void Start()
    {
        if (AsteroidsSO_Instance == null)
        {
            Debug.LogWarning("This script needs to be initialized with a class derived from ScriptableObjects");
        }

        Asteroids = AsteroidsSO_Instance.AsteroidPrefabs;
#if DEBUG_Spawner
        foreach (GameObject obj in Asteroids)
        {
            Debug.Log(obj.name);
        }
#endif
        SpawnAsteroidSystem(null, 0, 2);
    }

    private void SpawnAsteroidSystem(GameObject currentLevelAsteroid = null, int level = 0, int maxLevel = 2)
    {
        if (currentLevelAsteroid == null)
        {
            currentLevelAsteroid = Instantiate(Asteroids[level], Vector3.zero, Asteroids[level].transform.rotation);
        }

        GameObject childA = Instantiate(Asteroids[level + 1]);
        GameObject childB = Instantiate(Asteroids[level + 1]);
        childA.transform.SetParent(currentLevelAsteroid.transform, false);
        childB.transform.SetParent(currentLevelAsteroid.transform, false);
        
        if (level + 1< maxLevel)
        {
            SpawnAsteroidSystem(childA, level + 1, maxLevel);
            SpawnAsteroidSystem(childB, level + 1, maxLevel);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
