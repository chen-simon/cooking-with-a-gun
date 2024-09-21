using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    // Hard Coded for Fried Egg!

    public static RecipeManager main;
    public FriedEgg friedEgg;
    public Transform panLocation;

    int currentStage;

    public int flips;

    float eggMoveTime = 0.8f;
    public AudioSource sparkleSfx;

    [SerializeField] EggLauncher launcher;

    void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    void Start ()
    {
        Stage1();
    }
    void Update()
    {
        
    }

    public void Flip()
    {
        flips++;
        if (flips >= 5)
        {
            CompleteTask(1);
        }
    }

    public bool CompleteTask(int id)
    {
        if (id == currentStage)
        {            
            sparkleSfx.Play();
            Stage2();
            currentStage++;
            return true;
        }
        return false;
    }

    void Stage1()
    {
        launcher.StartLaunching();
    }

    void Stage2()
    {
        launcher.StopLaunching();
        StartCoroutine(Stage2Coroutine());
    }

    IEnumerator Stage2Coroutine()
    {
        yield return new WaitForSeconds(1.5f);
        Rigidbody rb = friedEgg.GetComponent<Rigidbody>();

        rb.angularVelocity = Vector3.zero;
        friedEgg.transform.DOMove(panLocation.position + new Vector3(0, 0.7f, 0), eggMoveTime);
        friedEgg.transform.DORotate(Vector3.zero, eggMoveTime);

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
