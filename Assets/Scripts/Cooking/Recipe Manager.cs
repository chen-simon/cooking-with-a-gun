using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    // Hard Coded for Fried Egg!

    public static RecipeManager main;
    public FriedEgg friedEgg;

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

    public bool CompleteTask(int id)
    {
        Stage2();
        return true;
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
        yield return new WaitForSeconds(2);
        Rigidbody rb = friedEgg.GetComponent<Rigidbody>();

        rb.angularVelocity = Vector3.zero;
        
    }
}
