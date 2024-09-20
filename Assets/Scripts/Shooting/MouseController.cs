using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void ShootInput()
    {
        GunController.main.Shoot();
    }

    public void UpdatePointerPosition()
    {
        // Convert the mouse position to a canvas position
        Vector2 mousePosition = Input.mousePosition;
            
        // Convert the mouse position to a canvas position
        Vector2 canvasPosition = Camera.main.ScreenToViewportPoint(mousePosition);
            
        canvasPosition.x = (canvasPosition.x * Screen.width) - Screen.width / 2;
        canvasPosition.y = (canvasPosition.y * Screen.height) - Screen.height / 2;
            
        GunController.main.UpdateCrosshairPostiton(canvasPosition);
    }
}
