using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{

    // Hard Coded for Fried Egg!

    public static RecipeManager main;

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

    public void CompleteTask(int id)
    {
        
    }

    void Stage1()
    {
        launcher.StartLaunching();

    }
}
