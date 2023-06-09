using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OffScreenWrapper))]
public class Bullet : MonoBehaviour
{
    static private Transform _BULLET_ANCHOR;
    static Transform BULLET_ANCHOR {
        get {
            if (_BULLET_ANCHOR == null) {
                GameObject go = new GameObject("BulletAnchor");
                _BULLET_ANCHOR = go.transform;
            }
            return _BULLET_ANCHOR;
        }
    }

    public float    bulletSpeed = 20;
    public float    lifeTime = 2;

    void Start()
    {
        transform.SetParent(BULLET_ANCHOR, true);

        // Set Bullet to self-destruct in lifeTime seconds
        Invoke("DestroyMe", lifeTime);

        // Set the velocity of the Bullet
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Asteroid"))
            DestroyMe();

        /*
        if (collision.collider.CompareTag("Asteroid"))
        {
            // Find Parent
            GameObject currentAsteroid = collision.collider.gameObject;
            while (currentAsteroid.transform.parent != null)
            {
                currentAsteroid = currentAsteroid.transform.parent.gameObject;
            }
            DestroyMe();
            GameObject[] nextTopLevels = new GameObject[2];

            for (int i = 0; i < currentAsteroid.transform.childCount; i++)
            {
                nextTopLevels[i] = currentAsteroid.transform.GetChild(i).gameObject;
                nextTopLevels[i].transform.parent = null;
            }
            Destroy(currentAsteroid);
            foreach (var go in nextTopLevels)
            {
                go.GetComponent<MeshCollider>().enabled = true;
            }

       

        }
        */
    }

}
