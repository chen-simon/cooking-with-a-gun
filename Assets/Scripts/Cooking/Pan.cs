using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Pan : MonoBehaviour, IShootable
{
    public float launchForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeShot(Vector3 knockbackForce)
    {
        if (FriedEggManager.main.friedEgg == null) return;

        Rigidbody eggRb = FriedEggManager.main.friedEgg.GetComponent<Rigidbody>();

        eggRb.AddForce(Vector3.up * launchForce);
        eggRb.angularVelocity = new Vector3(Random.Range(-10f, 10f),
                                            Random.Range(-10f, 10f),
                                            Random.Range(-10f, 10f));
        
        FriedEggManager.main.Flip();
    }
}
