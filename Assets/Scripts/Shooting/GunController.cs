using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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

    public LineRenderer gunTrail;

    // Used for screen shake upon shooting
    public float screenShakeMagnitude = 0.1f;
    public float screenShakeDuration = 0.1f;

    public bool inCooldown;
    public bool isReloading;
    public bool isFull = true;

    // Reference point for bulle trail -- should be used to reference a empty game object at where the bullet trail is supposed to start
    public Transform muzzle;

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
        InitialGun();
        if (gunTrail != null)
        {
            gunTrail.positionCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGunModelRotation();
    }
    public void InitialGun()
    {
        ammo = currentGun.ammoCapacity;
    }
    public void Shoot()
    {
        if (inCooldown) return;
        if (isReloading) return;
        if (TimeManager.main.isDayOver) return;
        if (ammo <= 0)
        {
            Reload();
            return;
        }

        ammo--;
        isFull = false;
        CameraController.main.Shake(
            currentGun.screenShakeDuration,
            currentGun.screenShakeMagnitude);
        gunshotClip.Play();
        muzzleFlash.Flash();

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, rayDirection, out hit, Mathf.Infinity, layerMask))
        {
            ShootEffect(hit);
            ShowGunTrail(hit.point);
            TimeManager.main.totalShot++;
            if (hit.collider.tag == "Level")
            {
                BulletHoleManager.main.CreateBulletHole(hit.point, Quaternion.LookRotation(hit.normal));
            }
            if(hit.collider.tag == "ValidTarget")
            {
                TimeManager.main.validShot++;
            }
        }
        else
        {
            // if didn't hit anything, show the trail to a point far in the distance
            ShowGunTrail(Camera.main.transform.position + rayDirection * 1000f);
        }
        StartCoroutine(Cooldown());
    }

    private void ShowGunTrail(Vector3 hitPoint)
    {
        if (gunTrail == null) return;

        StartCoroutine(AnimateGunTrail(hitPoint));
    }

    private IEnumerator AnimateGunTrail(Vector3 hitPoint)
    {
        float trailDuration = 0.1f;
        float elapsedTime = 0f;

        Vector3 startPosition = muzzle.position;

        Color startColor = gunTrail.startColor;
        Color endColor = gunTrail.endColor;

        while (elapsedTime < trailDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / trailDuration;

            gunTrail.positionCount = 2;

            // Set the bullet trail from muzzle to hit point over time
            gunTrail.SetPosition(0, startPosition);
            gunTrail.SetPosition(1, Vector3.Lerp(startPosition, hitPoint, progress));

            float alpha = Mathf.Lerp(1f, 0f, progress);
            gunTrail.startColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            gunTrail.endColor = new Color(endColor.r, endColor.g, endColor.b, alpha);

            yield return null;
        }

        gunTrail.positionCount = 0;
    }

    private IEnumerator FadeGunTrail()
    {
        float elapsedTime = 0f;
        float duration = 0.1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            gunTrail.startColor = new Color(gunTrail.startColor.r, gunTrail.startColor.g, gunTrail.startColor.b, alpha);
            gunTrail.endColor = new Color(gunTrail.endColor.r, gunTrail.endColor.g, gunTrail.endColor.b, alpha);
            yield return null;
        }

        gunTrail.positionCount = 0;
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
        isReloading = true;
        reloadAudio.Play();
        yield return new WaitForSeconds(currentGun.reloadTime);
        ammo = currentGun.ammoCapacity;
        isReloading = false;
        isFull = true;
    }

    public void Reload()
    {
        if (isReloading) return;
        if (ammo == currentGun.ammoCapacity) return;
        if (TimeManager.main.isDayOver) return;

        StopCoroutine(ReloadCoroutine());
        StartCoroutine(ReloadCoroutine());
    }
}
