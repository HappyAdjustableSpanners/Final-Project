using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotThrusterKeepUpright : MonoBehaviour {
    
    //Thruster rotation
    private Vector3 origRot;

    //Speed of rotation
    public float thrusterStabiliseSpeed;

    // Use this for initialization
    void Start () {
        origRot = transform.rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        //Find relative rot between current rot and rot facing the ground

        //Lerp towards the original z rotation
        transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, origRot.z)), Time.deltaTime * thrusterStabiliseSpeed);

    }
}
