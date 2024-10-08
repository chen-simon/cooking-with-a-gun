using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpwanButton : MonoBehaviour, IShootable
{
    [SerializeField]EggLauncher eggLauncher;
    public void TakeShot(Vector3 knockbackForce)
    {
        eggLauncher.LaunchEgg();
    }
}
