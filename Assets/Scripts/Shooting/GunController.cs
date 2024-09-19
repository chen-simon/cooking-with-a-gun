using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController main;

    public RectTransform crosshair;
    public Transform gunModel;
    public float distance;
    
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
        UpdateGunModelRotation();
    }

    public void UpdateCrosshairPostiton(Vector2 screenPosition)
    {
        crosshair.anchoredPosition = screenPosition;
    }

    void UpdateGunModelRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(crosshair.anchoredPosition + new Vector2(Screen.width / 2, Screen.height / 2));
        Vector3 target = ray.direction * distance + Camera.main.transform.position;

        Vector3 targetDirection = gunModel.transform.position - target;
        Vector3 newDirection = Vector3.RotateTowards(gunModel.forward, targetDirection, Mathf.PI, 0);
        gunModel.transform.rotation = Quaternion.LookRotation(newDirection);
    }    
}
