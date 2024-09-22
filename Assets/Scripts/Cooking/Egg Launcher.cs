using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggLauncher : MonoBehaviour
{
    public float cooldown = 5;
    public float launchForce = 5;

    public GameObject eggPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLaunching()
    {
        StartCoroutine(LaunchEgg());
    }

    public void StopLaunching()
    {
        StopAllCoroutines();
    }

    IEnumerator LaunchEgg()
    {
        yield return new WaitForSeconds(cooldown);

        GameObject egg = Instantiate(eggPrefab, transform.position, Quaternion.identity);

        Rigidbody rb = egg.GetComponent<Rigidbody>();

        rb.AddTorque(new Vector3(Random.Range(-10f, 10f),
                                 Random.Range(-10f, 10f),
                                 Random.Range(-10f, 10f)));
        
        rb.AddForce(launchForce * transform.forward);
        StartCoroutine(LaunchEgg());
    }
}
