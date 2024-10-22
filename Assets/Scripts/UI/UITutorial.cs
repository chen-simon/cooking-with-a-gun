using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UITutorial : MonoBehaviour
{

    // Currently hardcoded only for Fried Egg !!

    public string[] instructions;
    public GameObject[] thingsToUnhide;
    public string[] additionalInstrutctions;
    int currentAdditionalInstruction = 0;

    public TextMeshProUGUI instructionTextbox;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in thingsToUnhide)
        {
            obj.SetActive(false);
        }

        if (FriedEggManager.main == null)
            Disable();

        if (OrderManager.main.orderCounter > 0)
            Disable();

    }

    // Update is called once per frame
    void Update()
    {
        if (OrderManager.main.orderCounter > 0)
        {
            if (additionalInstrutctions.Length >= currentAdditionalInstruction)
            {
                Disable();
            }

            // Additional Instructions
            instructionTextbox.text = instructions[currentAdditionalInstruction];
            /// TODO: Finish later ...
        }

        // Main Instructions

        instructionTextbox.text = instructions[FriedEggManager.main.currentStage];
    }


    void Disable()
    {
        foreach (GameObject obj in thingsToUnhide)
        {
            obj.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
