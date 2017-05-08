using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlaceItem : MonoBehaviour {

    //When press touchpad, instantiate object and make it follow controller

    //Controller
    private SteamVR_TrackedObject trackedObj;
    private Transform controllerTip;

    //State
    private enum State { idle, creatingItem, placingItem }
    private State state = State.idle;


    //Instantiated object
    private GameObject obj;
    private Material obj_Mat;

    //Object to place
    public GameObject item;

	// Use this for initialization
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        controllerTip = transform.Find("ControllerTip");
    }
	
	// Update is called once per frame
	void Update () {

        //Setup controller
        SteamVR_Controller.Device controller = SteamVR_Controller.Input((int)trackedObj.index);

        if (state == State.idle)
        {
            //If touchpad pressed, change state and instantiate object
            if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            {
                //Instantiate the object
                obj = Instantiate(item, controllerTip.position, controllerTip.rotation);

                //Change state to creating item phase
                state = State.creatingItem;
            }
        }
        else if (state == State.creatingItem)
        {
            //If our object is not null
            if (obj != null)
            {
                //Record the original material, then change it to a transparent one while placing
                obj_Mat = obj.GetComponent<MeshRenderer>().material;
                obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/M_pickup_tinted");

                //Make the object a trigger so it doesn't collide with anything
                obj.GetComponent<BoxCollider>().isTrigger = true;
             
                //Change state to placing phase
                state = State.placingItem;
            }
            else
            {
                Debug.Log("Object to place is null");
            }
        }
        else if (state == State.placingItem)
        {
            //Make the object follow the controller tip position
            obj.transform.position = controllerTip.position;

            //If the touchpad is released, place item
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                //Restore original material
                obj.GetComponent<MeshRenderer>().material = obj_Mat;

                //Change state to idle phase
                state = State.idle;
            }
        }
    }
}
