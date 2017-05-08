using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitRotation : MonoBehaviour {

    //Specify clamp amount
    public float limitX, limitY, limitZ = -1f;
     
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (limitZ != -1)
        {
            float rotZ = Mathf.Clamp(transform.rotation.eulerAngles.z, -limitZ, limitZ);

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, rotZ);
        }
	}
}
