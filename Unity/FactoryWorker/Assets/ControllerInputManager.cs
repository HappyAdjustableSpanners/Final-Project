using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {


    //Button states
    public bool triggerDown  = false;
    public bool triggerTouch = false;
    public bool triggerUp    = false;

    public bool dPadDown  = false;
    public bool dPadTouch = false;
    public bool dPadUp    = false;

    public bool dPadLeftTouch = false;
    public bool dPadRightTouch = false;

    public bool menuDown  = false;
    public bool menuTouch = false;
    public bool menuUp    = false;

    //Controller 
    public Valve.VR.InteractionSystem.HandPainter controller;

    //Button ids
    private Valve.VR.EVRButtonId trigger = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId dPad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private Valve.VR.EVRButtonId menu = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (controller.GetPressDown(trigger))
        {
            triggerDown = true;
        }
        else
            triggerDown = false;

        if (controller.GetTouch(trigger))
        {
            triggerTouch = true;
        }
        else
            triggerTouch = false;

        if (controller.GetTouch(dPad))
        {
            dPadTouch = true;
        }
        else
            dPadTouch = false;

        if (controller.GetDPadTouchLeft())
        {
            dPadLeftTouch = true;
        }
        else
            dPadLeftTouch = false;

        if (controller.GetDPadTouchRight())
        {
            dPadRightTouch = true;
        }
        else
            dPadRightTouch = false;
           
        if (controller.GetTouch(menu))
        {
            menuTouch = true;
        }
        else
            menuTouch = false;
    }
}
