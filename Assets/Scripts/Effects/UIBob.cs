using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBob : MonoBehaviour
{
    public float frequency;
    public float amplitude;

    Vector2 initPostition;
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initPostition = rectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = initPostition;
        pos.y += amplitude *  Mathf.Sin(Time.time * frequency);
        rectTransform.localPosition = pos;
    }
}
