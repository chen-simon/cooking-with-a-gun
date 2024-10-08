using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLauncher : MonoBehaviour
{
    public float cooldown = 5;
    public float launchForce = 5;

    public GameObject eggPrefab;
    public GameObject egg;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchEgg()
    {
        if(egg == null && active){
        egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
        
        Rigidbody rb = egg.GetComponent<Rigidbody>();

        // Add random rotation to the egg
        rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                 Random.Range(-10f, 10f),
                                 Random.Range(-10f, 10f)));

        // Add force to launch the egg forward
        rb.AddForce(launchForce * transform.forward);
        Egg eggScript = egg.GetComponent<Egg>();
            eggScript.Initialize(this);
        }
    }
    public void OnEggDestroyed()
    {
        egg = null;
    }
}
