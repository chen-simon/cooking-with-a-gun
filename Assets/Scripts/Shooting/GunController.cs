using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController main;

    public RectTransform crosshair;
    public Transform gunModel;
    public float distance;

    public int ammo;

    public Gun currentGun;
    public MuzzleFlash muzzleFlash;

    // Used for screen shake upon shooting
    public float screenShakeMagnitude = 0.1f;
    public float screenShakeDuration = 0.1f;

    bool inCooldown;

    Vector3 rayDirection;
    
    public LayerMask layerMask;

    [SerializeField] AudioSource gunshotClip;
    [SerializeField] AudioSource reloadAudio;
    
    void Awake()
    {
        if (main) Destroy(gameObject);
        else main = this;
    }

    void Start()
    {
        ammo = currentGun.ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGunModelRotation();
    }

    public void Shoot()
    {
        if (inCooldown) return;

        if (ammo <= 0)
        {
            Reload();
            return;
        }

        ammo--;
        
        CameraController.main.Shake(
            currentGun.screenShakeDuration,
            currentGun.screenShakeMagnitude);
        gunshotClip.Play();
        muzzleFlash.Flash();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out hit, Mathf.Infinity, layerMask))
        {
            ShootEffect(hit);
            if (hit.collider.tag == "Level")
            {
                BulletHoleManager.main.CreateBulletHole(hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        StartCoroutine(Cooldown());
    }

    void ShootEffect(RaycastHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb == null) return;

        Vector3 knockbackForce = currentGun.knockbackForce * Mathf.Abs(Vector3.Dot(hit.normal, rayDirection)) * -hit.normal;
                
        rb.AddForce(knockbackForce);

        AudioSource audioSource = rb.GetComponent<AudioSource>();
        if (audioSource)
            audioSource.Play();

        IShootable shootable = rb.GetComponent<IShootable>();
        if (shootable != null)
            shootable.TakeShot(knockbackForce);
        
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

    IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(1f / currentGun.firerate);
        inCooldown = false;
    }

    IEnumerator ReloadCoroutine()
    {
        reloadAudio.Play();
        yield return new WaitForSeconds(currentGun.reloadTime);
        ammo = currentGun.ammoCapacity;
    }

    void Reload()
    {
        StopCoroutine(ReloadCoroutine());
        StartCoroutine(ReloadCoroutine());
    }
}
