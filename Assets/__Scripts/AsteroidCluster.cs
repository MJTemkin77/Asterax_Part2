using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCluster : MonoBehaviour
{

    public void SetSpeedAndRotation(float speed, float rotation, int numScale)
    {
        BaseSpeed = speed;
        rotationSpeed = rotation;
        currentSize = numScale;
        currentRotationSpeed = rotationSpeed / currentSize;
        currentSpeed = BaseSpeed / currentSize;
    }
    private float BaseSpeed;
    private float rotationSpeed;


    private int currentSize = 0;
    private float currentRotationSpeed = 0;
    private float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(transform.right * currentSpeed * Time.deltaTime);
            transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            // Find Parent
            GameObject currentAsteroid = this.gameObject;
            List<GameObject> nextTopLevels  = new List<GameObject>();

            foreach (Transform childAsteroidTrans in currentAsteroid.transform)
            {
                nextTopLevels.Add(childAsteroidTrans.gameObject);
            }
            foreach (GameObject nextTopLevel in nextTopLevels)
            {
                nextTopLevel.transform.SetParent(null, false);
                nextTopLevel.AddComponent<AsteroidCluster>();
            }
            
            Destroy(currentAsteroid);
            foreach (var go in nextTopLevels)
            {
                go.GetComponent<MeshCollider>().enabled = true;
            }



        }
    }
}
