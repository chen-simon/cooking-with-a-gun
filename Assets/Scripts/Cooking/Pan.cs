using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour, IShootable
{
    public float launchForce;
    public float angularVelocityAmount = 60f;
    private Rigidbody panRb;
    [SerializeField] GameObject oilSplatterPrefab;

    // Start is called before the first frame update
    void Start()
    {
        panRb = GetComponent<Rigidbody>();
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

        AudioSource eggAudio = FriedEggManager.main.friedEgg.GetComponent<AudioSource>();
        Instantiate(oilSplatterPrefab, transform);
        eggAudio.Play();
        AddAngularVelocity();
    }
    void AddAngularVelocity()
    {
        panRb.angularVelocity = new Vector3(0, angularVelocityAmount, 0);
    }
}
