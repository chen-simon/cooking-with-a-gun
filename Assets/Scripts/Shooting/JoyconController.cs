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
	
	public RectTransform pointer;
    void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        aim_offset = Quaternion.identity;
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
				// Debug.Log ("Right trigger pressed");
				joycons[jc_ind].SetRumble(160, 320, 0.6f, 75);  // Short 
				gunshot_clip.Play();
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

			if (j.GetButtonDown (Joycon.Button.DPAD_DOWN)) {
				Debug.Log ("Rumble");

				// Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
				// https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

				j.SetRumble (160, 320, 0.6f, 200);

				// The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
                // (Useful for dynamically changing rumble values.)
				// Then call SetRumble(0,0,0) when you want to turn it off.
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

            Quaternion desiredRot = orientation;
            
            // Map orientation to screen pointer movement
            UpdatePointerPosition(orientation);
            
            if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
			{
				aim_offset = Quaternion.Inverse(desiredRot);
			}
			
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, aim_offset * desiredRot, 20f * Time.deltaTime);
			
			// https://github.com/Looking-Glass/JoyconLib/issues/8
			// gameObject.transform.Rotate(90,0,0,Space.World); 
			// Debug.Log(gameObject.transform.rotation);
        }
	    
    }
    
    // Method to map the Joycon orientation to the pointer's position on the screen
    void UpdatePointerPosition(Quaternion joyconOrientation)
    {
	    // Convert the orientation to a forward vector (direction the Joycon is pointing)
	    Vector3 forward = joyconOrientation * Vector3.forward;
		Vector3 eulerOrientation = orientation.eulerAngles;

	    // You can use the x and y axes from the forward vector to calculate pointer position
	    // Map these values to the screen (e.g., multiplying by some sensitivity factor)

		float DEG_TO_RAD = 0.017453329252f;

	    // Assuming you want to move the pointer in 2D space (x and y coordinates of the screen)
	    float newX = Mathf.Sin(transform.rotation.eulerAngles.y * DEG_TO_RAD) * pointerSensitivity;
	    float newY = Mathf.Sin(transform.rotation.eulerAngles.x * DEG_TO_RAD) * -pointerSensitivity;

	    // Set the pointer's anchored position (used for UI elements in Canvas)
	    GunController.main.UpdateCrosshairPostiton(new Vector2(newX, newY));
    }

}