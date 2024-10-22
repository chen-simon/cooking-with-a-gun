using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour, IShootable
{
    public float launchForce;
    public float angularVelocityAmount = 60f;
    private Rigidbody panRb;
    [SerializeField] ParticleSystem oilSplatter;
    [SerializeField] ParticleSystem steamSplatter;
    [SerializeField] AudioSource panSizzleAudio;

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

        panSizzleAudio.Play();

        oilSplatter.Play();
        steamSplatter.Play();
        AddAngularVelocity();
    }
    void AddAngularVelocity()
    {
        panRb.angularVelocity = new Vector3(0, angularVelocityAmount, 0);
    }
}
