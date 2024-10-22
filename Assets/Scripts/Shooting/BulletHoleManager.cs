using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleManager : MonoBehaviour
{

    // Singleton object
    public static BulletHoleManager main;

    // Prefab
    public GameObject bulletHolePrefab;
    
    // Pool of bullet holes
    private Queue<GameObject> bulletHoles;
    
    // Max number of bullet holes before recycling from the pool
    public int maxBulletHoles = 10;
    
    private void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
        
        if (bulletHolePrefab == null)
        {
            Debug.LogError("Bullet Hole Prefab is not set in the BulletHoleManager");
        }
        
        bulletHoles = new Queue<GameObject>();
    }
    
    public void CreateBulletHole(Vector3 position, Quaternion rotation)
    {
        if (bulletHoles.Count >= maxBulletHoles)
        {
            // Recycle the oldest bullet hole
            GameObject bulletHole = bulletHoles.Dequeue();
            bulletHole.transform.position = position;
            bulletHole.transform.rotation = rotation;
            bulletHole.GetComponentInChildren<ParticleSystem>().Play();
            bulletHoles.Enqueue(bulletHole);
        } else {
            // Create a new bullet hole as children of this object
            GameObject bulletHole = Instantiate(bulletHolePrefab, position, rotation, this.transform);
            bulletHoles.Enqueue(bulletHole);
        }
    }
}
