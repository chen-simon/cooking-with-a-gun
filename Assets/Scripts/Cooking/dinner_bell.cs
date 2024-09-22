using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinnerBell : MonoBehaviour, IShootable
{
    private bool active = false;
    [SerializeField] private FriedEggManager FriedEggManager;
    public void TakeShot(Vector3 knockbackForce)
    {
        if(active)
        {
            FriedEggManager.CompleteTask(1);
        }
    }

    public void active_hit()
    {
        active = true;
    }

    public void deactive_hit()
    {
        active = false;
    }
}
