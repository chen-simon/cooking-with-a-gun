using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpwanButton : MonoBehaviour, IShootable
{
    [SerializeField]EggLauncher eggLauncher;
    public void TakeShot(Vector3 knockbackForce)
    {
        StartCoroutine(PressButtonEffect2());
        eggLauncher.LaunchEgg();
    }

    private IEnumerator PressButtonEffect()//this effect scales down the button
    {
        // Scale the button down to simulate a press
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        yield return new WaitForSeconds(0.2f);
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private IEnumerator PressButtonEffect2()//this effect offset the button
    {
        Vector3 originalPosition = transform.position;
        // Move the button backward slightly
        Vector3 pressedPosition = originalPosition - transform.forward * 0.3f;
        transform.position = pressedPosition;
        yield return new WaitForSeconds(0.2f);
        transform.position = originalPosition;
    }
}
