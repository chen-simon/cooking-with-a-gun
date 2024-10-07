using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Glow : MonoBehaviour
{

    // Currently hardcoded only for Fried Egg !!

    public int stepToGlow;

    public Color bright;
    public Color dark;
    public float minWidth;
    public float maxWidth;
    public float frequency;

    public QuickOutline.Outline outline;

    // Start is called before the first frame update
    void Start()
    {        
        if (FriedEggManager.main == null)
            enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FriedEggManager.main.currentStage != stepToGlow)
        {
            outline.enabled = false;
            return;
        }

        outline.enabled = true;

        float phase = Mathf.Sin(Time.time * frequency);
        Color color = Color.Lerp(dark, bright, phase);
        float outlineWidth = Mathf.Lerp(minWidth, maxWidth, phase);

        outline.OutlineColor = color;
        outline.OutlineWidth = outlineWidth;

    }
}
