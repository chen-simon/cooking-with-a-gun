using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerBell : MonoBehaviour, IShootable
{
    [SerializeField] private FriedEggManager FriedEggManager;
    public void TakeShot(Vector3 knockbackForce)
    {
        FriedEggManager.CompleteTask(2);
    }
}
