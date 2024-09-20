using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController main;

    public RectTransform crosshair;
    public Transform gunModel;
    public float distance;

    public Gun currentGun;
    public MuzzleFlash muzzleFlash;

    Vector3 rayDirection;
    
    public LayerMask layerMask;

    [SerializeField] AudioSource gunshotClip;
    
    void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGunModelRotation();
    }

    public void Shoot()
    {
        gunshotClip.Play();
        muzzleFlash.Flash();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out hit, Mathf.Infinity, layerMask))
        {
            
            if (TryShootEffect(hit) == false) {
                TryShootPhysics(hit);
            }
        }
    }

    bool TryShootEffect(RaycastHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb == null) return false;

        IShootable shootable = null;
        try {
            shootable = rb.GetComponent<IShootable>();
            shootable.TakeShot();
            return true;
        }
        catch
        {
            return false;
        }
    }

    bool TryShootPhysics(RaycastHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb == null) return false;

        rb.AddForce(-hit.normal * Mathf.Abs(Vector3.Dot(hit.normal, rayDirection)) * currentGun.knockbackForce);
        return true;
    }

    public void UpdateCrosshairPostiton(Vector2 screenPosition)
    {
        crosshair.anchoredPosition = screenPosition;
    }

    void UpdateGunModelRotation()
    {
        float scale = Screen.width / 1920f;

        Vector2 scaledCrosshairCoords = 
            new Vector2(crosshair.anchoredPosition.x * scale + Screen.width / 2,
                        crosshair.anchoredPosition.y * scale + Screen.height / 2);

        Ray ray = Camera.main.ScreenPointToRay(scaledCrosshairCoords);
        rayDirection = ray.direction;

        Vector3 target = ray.direction * distance + Camera.main.transform.position;

        Vector3 targetDirection = gunModel.transform.position - target;
        Vector3 newDirection = Vector3.RotateTowards(gunModel.forward, targetDirection, Mathf.PI, 0);
        gunModel.transform.rotation = Quaternion.LookRotation(newDirection);
    }    
}
