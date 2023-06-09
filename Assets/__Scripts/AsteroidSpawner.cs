#define DEBUG_Spawner
using UnityEngine;


public class AsteroidSpawner : MonoBehaviour
{
    [Header("Initialize with the Asteroid Scriptable Object")]
    [SerializeField]
    private AsteroidsSO AsteroidsSO_Instance;

    [SerializeField]
    private float BaseSpeed;

    [SerializeField]
    private float rotationSpeed;

    private GameObject[] Asteroids;

    private int currentSize = 0;
    private float currentRotationSpeed = 0;
    private float currentSpeed;

    private GameObject currentAsteroid;

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

    /// <summary>
    /// Recursive generation of an Asteroid cluster.
    /// Need to work on making them adjacent and scaling their size by each generation.
    /// </summary>
    /// <param name="currentLevelAsteroid"></param>
    /// <param name="level"></param>
    /// <param name="maxLevel"></param>
    private void SpawnAsteroidSystem(GameObject currentLevelAsteroid = null, int level = 0, int maxLevel = 2)
    {
        int numScale = maxLevel + 1 - level;
        if (currentLevelAsteroid == null)
        {
            Debug.LogFormat("numScale = {0}", numScale);
            currentLevelAsteroid = Instantiate(Asteroids[level], this.transform.position, Asteroids[level].transform.rotation);
            currentLevelAsteroid.transform.localScale *= numScale;
            currentLevelAsteroid.GetComponent<MeshCollider>().enabled = true;
            currentSize = numScale;
            currentRotationSpeed = rotationSpeed / currentSize;
            currentSpeed = BaseSpeed / currentSize;
            currentAsteroid = currentLevelAsteroid;
        }


        Mesh mesh = currentLevelAsteroid.GetComponent<MeshFilter>().mesh;
        Vector2[] vertices = mesh.uv;
                
        int v0 = Random.Range(0, vertices.Length);
        int v1 = Random.Range(0, vertices.Length);

        GameObject childA = Instantiate(Asteroids[level + 1], vertices[v0], Asteroids[level + 1].transform.rotation);
        GameObject childB = Instantiate(Asteroids[level + 1], vertices[v1], Asteroids[level + 1].transform.rotation);
        childA.GetComponent<MeshCollider>().enabled = false;
        childB.GetComponent<MeshCollider>().enabled = false;
        // Need to position adjacent to parent

        numScale--;
        Debug.LogFormat("numScale = {0}", numScale);
        childA.transform.localScale *= numScale;
        childB.transform.localScale *= numScale;


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
        if (currentAsteroid != null)
        {
            currentAsteroid.transform.Translate(currentAsteroid.transform.right * currentSpeed * Time.deltaTime);
            currentAsteroid.transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
        }
    }
}
