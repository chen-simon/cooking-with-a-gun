using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController main;

    public RectTransform crosshair;
    
    void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCrosshairPostiton(Vector2 screenPosition)
    {
        crosshair.anchoredPosition = screenPosition;
    }
}
