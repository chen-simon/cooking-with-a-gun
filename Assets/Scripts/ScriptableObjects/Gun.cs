using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cooking With a Gun/Guns")]
public class Gun : ScriptableObject
{
    new public string name;
    public bool isAuto;
    public int ammoCapacity;
    public float reloadTime;
    public float spread;

    public float knockbackForce;
    public float rumbleMagnitude;
    public int rumbleTime;  // in milliseconds
    public float screenShakeMagnitude;
    public float screenShakeDuration;  // in seconds

    public float firerate;

}
