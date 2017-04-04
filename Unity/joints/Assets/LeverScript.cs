using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour {

    /*
     * Calculate relative direction between mouse click position and the base of the lever,
     * set the hinge join target angle to the relative direction
     */

    //Hinge joint
    private HingeJoint leverHingeJoint;

    private GameObject leverShaft, leverBase, leverHandle;

	// Use this for initialization
	void Start () {

        //Get the lever shaft and base
        leverShaft = GameObject.Find("shaft"); leverBase = GameObject.Find("base"); leverHandle = GameObject.Find("handle");

        //Get hinge joint
        leverHingeJoint = leverShaft.GetComponent<HingeJoint>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDrag()
    {
        //Calculate relative position
        Vector3 relativeDir = leverBase.transform.position - Input.mousePosition;

        //Add velocity to lever handle in the relative direction
        GetComponent<Rigidbody>().AddForce(relativeDir * -0.01f, ForceMode.VelocityChange);

        //Set the new spring target position
        //JointSpring spr = leverHingeJoint.spring;
        //spr.targetPosition = relativeDir.x;
        //leverHingeJoint.spring = spr;
    }
}
