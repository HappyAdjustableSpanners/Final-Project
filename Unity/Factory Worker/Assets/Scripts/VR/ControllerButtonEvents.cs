using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtonEvents : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public bool triggerPress;
    public bool menuButtonPress;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Get reference to the controller
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        //If the trigger is pressed
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            triggerPress = true;
        }

    }
}
