using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MouseManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Print out the position of the mouse
        // When the mouse moves, print out the position of the mouse
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            UpdatePointerPosition();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            ShootInput();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ReloadInput();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartNextDayInput();
        }
    }

    public void ShootInput()
    {
        GunController.main.Shoot();
    }

    public void ReloadInput()
    {
        GunController.main.Reload();
    }

    public void StartNextDayInput()
	{
		TimeManager.main.StartNextDay();
	}
    public void UpdatePointerPosition()
    {
            
        // Convert the mouse position to a canvas position
        float scaling =  1920f / Screen.width;
        Vector2 canvasPosition = Input.mousePosition * scaling;
            
        canvasPosition.x = canvasPosition.x - (Screen.width / 2) * scaling;
        canvasPosition.y = canvasPosition.y - (Screen.height / 2) * scaling;
        
        GunController.main.UpdateCrosshairPostiton(canvasPosition);
    }
}
