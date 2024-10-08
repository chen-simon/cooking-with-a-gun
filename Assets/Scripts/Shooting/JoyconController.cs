using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconController : MonoBehaviour {
	
	private List<Joycon> joycons;

    // Values made available via Unity
    public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jc_ind = 0;
    public Quaternion orientation;
    private Quaternion aim_offset;
	public float pointerSensitivity = 1600f;

    public AudioSource gunshot_clip;

	public GameObject destroyedPrefab;
	
	void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        aim_offset = Quaternion.Euler(90, 120, 120);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
		
		gameObject.transform.rotation = orientation;
	}

	// Update is called once per frame
    void Update () {
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];
			// GetButtonDown checks if a button has been pressed (not held)
            if (j.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
	            ShootInput();
            }
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp (Joycon.Button.SHOULDER_2))
			{
				// Debug.Log ("Right trigger released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton (Joycon.Button.SHOULDER_2))
			{
				// Debug.Log ("Right trigger held");
			}

			if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
				Debug.Log ("DPAD_RIGHT pressed");
				// GetStick returns a 2-element vector with x/y joystick components
				Debug.Log(string.Format("Stick x: {0:N} Stick y: {1:N}",j.GetStick()[0],j.GetStick()[1]));
            
				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				j.Recenter ();
			}
			// GetButtonDown checks if a button has been released
			if (j.GetButtonUp (Joycon.Button.DPAD_RIGHT))
			{
				Debug.Log ("DPAD_RIGHT released");
			}
			// GetButtonDown checks if a button is currently down (pressed or held)
			if (j.GetButton (Joycon.Button.DPAD_RIGHT))
			{
				Debug.Log ("DPAD_RIGHT held");
			}
			if (j.GetButtonDown (Joycon.Button.DPAD_LEFT))
			{
				Debug.Log ("DPAD_LEFT pressed");
				ReloadInput();
				j.SetRumble (100, 150, 0.6f, 100);
			}
			if (j.GetButtonDown (Joycon.Button.DPAD_LEFT))
			{
				Debug.Log ("DPAD_LEFT released");
			}
			if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("DPAD_DOWN pressed");
				StartNextDayInput();
			}
			if (j.GetButtonUp (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("DPAD_DOWN released");
			}

            stick = j.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = j.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = j.GetAccel();
            
            orientation = j.GetVector();
            
            if (j.GetButton(Joycon.Button.DPAD_UP)){
				gameObject.GetComponent<Renderer>().material.color = Color.red;
			}
			else
			{
				gameObject.GetComponent<Renderer>().material.color = Color.blue;
			}

			Quaternion desiredRotation = aim_offset * orientation;

            if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
				// Only recenters on the y axis
				aim_offset = Quaternion.RotateTowards(gameObject.transform.rotation, Quaternion.Euler(90, 120, 120), 180);
			}			
			
			// Smoothly interpolate the object's rotation to the desired rotation
            gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, desiredRotation, 20f * Time.deltaTime);
	    	
            // Map orientation to screen pointer movement
			UpdatePointerPosition(orientation, transform.rotation);
			
			// https://github.com/Looking-Glass/JoyconLib/issues/8
			// gameObject.transform.Rotate(90,0,0,Space.World); 
			// Debug.Log(gameObject.transform.rotation);
        }
	    
    }
    
    // Method to map the Joycon orientation to the pointer's position on the screen
    void UpdatePointerPosition(Quaternion joyconOrientation, Quaternion rotation)
    {
	    // Convert the orientation to a forward vector (direction the Joycon is pointing)
	    // Rotate the entire joycon orientation by 90 degrees to match the orientation of the gun model
	    
	    Quaternion new_orientation = Quaternion.Euler(-90, 0, 0) * joyconOrientation;
	    
	    Vector3 forward = new_orientation * Vector3.forward;
		Vector3 eulerOrientation = orientation.eulerAngles;

	    // You can use the x and y axes from the forward vector to calculate pointer position
	    // Map these values to the screen (e.g., multiplying by some sensitivity factor)

		float DEG_TO_RAD = 0.017453329252f;

	    // Assuming you want to move the pointer in 2D space (x and y coordinates of the screen)
	    float newX = Mathf.Sin(rotation.eulerAngles.y * DEG_TO_RAD) * pointerSensitivity;
	    float newY = Mathf.Sin(rotation.eulerAngles.x * DEG_TO_RAD) * -pointerSensitivity;
	    
	    // Set the pointer's anchored position (used for UI elements in Canvas)
	    GunController.main.UpdateCrosshairPostiton(new Vector2(newX, newY));
    }

    public void ShootInput()
    {
	    // Debug.Log ("Right trigger pressed");
	    Gun currentGun = GunController.main.currentGun;

	    joycons[jc_ind].SetRumble(160, 320, currentGun.rumbleMagnitude, currentGun.rumbleTime); // Short 
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
}