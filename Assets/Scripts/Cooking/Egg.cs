using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour, IShootable
{
    public GameObject eggShellPrefab;
    public GameObject eggInsidesPrefab;
    public float destroy_y = -1f;
    private EggLauncher eggLauncher;
    private SFXList sfxList;
    private AudioClip selectedClip;
    AudioSource audioSource;
    public void Initialize(EggLauncher launcher)
    {
        eggLauncher = launcher; // Store reference to the launcher
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < destroy_y)
        {
            Destroy(gameObject);
        }
    }

    public void TakeShot(Vector3 knockbackForce)
    {
        GameObject eggShell = Instantiate(eggShellPrefab, transform.position, transform.rotation);
        GameObject eggInsides = Instantiate(eggInsidesPrefab, transform.position, transform.rotation);

        Rigidbody rb = GetComponent<Rigidbody>();
        eggShell.GetComponent<Rigidbody>().velocity = rb.velocity;
        eggShell.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-10f, 10f),
                                                                        Random.Range(-10f, 10f),
                                                                        Random.Range(-10f, 10f));
        eggShell.GetComponent<Rigidbody>().AddForce(knockbackForce);

        eggInsides.GetComponent<Rigidbody>().velocity = rb.velocity;
        eggInsides.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-10f, 10f),
                                                                        Random.Range(-10f, 10f),
                                                                        Random.Range(-10f, 10f));
        eggInsides.GetComponent<Rigidbody>().AddForce(knockbackForce);

        Destroy(gameObject);
    }

    private void PlayClip(){
        if (sfxList != null  && sfxList.sfxClips.Length > 0 )
        {
            int randomIndex = Random.Range(0, sfxList.sfxClips.Length);
            selectedClip = sfxList.sfxClips[randomIndex];
        }
        if (audioSource != null)
        {
            audioSource.PlayOneShot(selectedClip);
        }
        else
        {
            Debug.LogWarning("AudioSource component is missing on " + gameObject.name);
        }
    }
}
