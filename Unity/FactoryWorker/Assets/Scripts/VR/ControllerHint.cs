using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHint : MonoBehaviour {

    //The local position to the controller, at which the hint is to be displayed
    public Transform hintPosition;

    public int handIndex;

    void Awake()
    {
    }
	// Use this for initialization
	void Start () {

        if (handIndex == 1)
        {
            //Set the controller as parent
            transform.SetParent(GameObject.Find("Player").transform.Find("SteamVRObjects/Hand1"));
        }
        else
        {
            //Set the controller as parent
            transform.SetParent(GameObject.Find("Player").transform.Find("SteamVRObjects/Hand2"));
        }

        //Set the transforms
        transform.localPosition = hintPosition.position;
        transform.rotation = hintPosition.rotation;
        transform.localScale = hintPosition.localScale;
    }
}
