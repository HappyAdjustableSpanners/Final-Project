using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHint : MonoBehaviour {

    //The local position to the controller, at which the hint is to be displayed
    public Transform hintPosition;

    //Which hand should the hint appear on
    public int handIndex;

	// Use this for initialization
	void Start () {

        //Set the position based on the hand chosen
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

        //Set the position of the hint based on our hintPosition prefab
        transform.localPosition = hintPosition.position;
        transform.rotation = hintPosition.rotation;
        transform.localScale = hintPosition.localScale;
    }
}
