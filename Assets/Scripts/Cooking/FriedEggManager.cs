using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FriedEggManager : MonoBehaviour
{

    // Hard Coded for Fried Egg!

    public static FriedEggManager main;
    public FriedEgg friedEgg;
    public DinnerBell dinnerBell;
    public Transform panLocation;
    public Transform exitLocation;
    [SerializeField]private OrderManager orderManager;

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
            
            // Hard coded cases
            if (id == 0) 
            { 
                sparkleSfx.Play();
                Stage2(); 
            }
            if (id == 1)
            {

            }
            if (id == 2)
            {
                StartCoroutine(FinishOrder());
                Stage1();

            }
            currentStage = (currentStage + 1) % 3;
            sparkleSfx.Play();
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

    IEnumerator FinishOrder()
    {
        flips = 0;
        Transform eggToMove = friedEgg.transform;
        friedEgg = null;
        yield return new WaitForSeconds(0);
        eggToMove.DOMove(exitLocation.position, eggMoveTime);
        orderManager.OrderFinished();
        orderManager.OrderUpdate();
    }


    IEnumerator Stage2Coroutine()
    {
        yield return new WaitForSeconds(1f);
        Rigidbody rb = friedEgg.GetComponent<Rigidbody>();

        rb.angularVelocity = Vector3.zero;
        friedEgg.transform.DOMove(panLocation.position + new Vector3(0, 0.7f, 0), eggMoveTime);
        friedEgg.transform.DORotate(Vector3.zero, eggMoveTime);

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}
