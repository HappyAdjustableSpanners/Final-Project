using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAnimationBehaviour : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private Animator anim;

	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //Setup controller
        SteamVR_Controller.Device controller = SteamVR_Controller.Input((int)trackedObj.index);

        //Check for trigger press
        if (controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            anim.SetBool("Grabbing", true);
        }
        else
            anim.SetBool("Grabbing", false);

	}
}
