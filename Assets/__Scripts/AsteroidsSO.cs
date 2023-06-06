using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroids", menuName ="ScriptableObjects/AsteroidsScriptableObject", order =1)]
public class AsteroidsSO : ScriptableObject
{
    [Header("Initialize with all the available asteroid prefabs.")]
    
    public GameObject[] AsteroidPrefabs;

   
}
