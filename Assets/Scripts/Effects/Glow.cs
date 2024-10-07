using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{

    // Currently hardcoded only for Fried Egg !!

    public bool alwaysGlows;
    public int stepToGlow;
    int glowLayer = 7;
    int noGlowLayer = 0;
    

    public GameObject[] meshes;

    // Start is called before the first frame update
    void Start()
    {
        if (FriedEggManager.main == null)
            enabled = false;

        if (!alwaysGlows && OrderManager.main.orderCounter > 0)
            enabled = false;
        
        if (meshes[0].layer == 6)
        {
            // Gun only
            glowLayer = 8;
            noGlowLayer = 6;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alwaysGlows && OrderManager.main.orderCounter > 0)
            enabled = false;

        
        if (FriedEggManager.main.currentStage == stepToGlow)
        {
            foreach(GameObject obj in meshes)
                obj.layer = glowLayer;
        }
        else
        {
            foreach(GameObject obj in meshes)
                obj.layer = noGlowLayer;
        }
    }
}
