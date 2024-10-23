using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    public float frequency;
    public float amplitude;

    Vector3 initPostition;

    // Start is called before the first frame update
    void Start()
    {
        initPostition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = initPostition;
        pos.y += amplitude *  Mathf.Sin(Time.time * frequency);
        transform.localPosition = pos;
    }
}
