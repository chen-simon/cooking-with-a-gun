using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController main;

    public RectTransform crosshair;
    public Transform gunModel;
    public float distance;

    [SerializeField] AudioSource gunshotClip;
    
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

    public void Shoot()
    {
        gunshotClip.Play();

        // ... TODO: add more logic, 

        // Hit all IShootables and call TakeShot();
        // if didn't hit IShootable, but hit physics object, add force
        Ray ray = Camera.main.ScreenPointToRay(crosshair.anchoredPosition + new Vector2(Screen.width / 2, Screen.height / 2));
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
