using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour {

    public GameObject objToSpawn;

    public SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //Create cube
            Instantiate(objToSpawn, device.transform.pos, Quaternion.identity);
        }
	}
}
