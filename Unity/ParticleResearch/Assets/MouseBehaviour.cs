using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehaviour : MonoBehaviour {

    public GameObject objToSpawn;

    //Detect mouse click, if detected, check if click position was inside dimensions of plane. If so, spawn a cube clamped to the nearest edge
    public float xLim = 2f;
    public float zLim = 2f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        //If left mouse button clicked
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Clicked");

            //Get the click position and clamp it to within range
            Vector3 clickPos = new Vector3( Mathf.Clamp( Input.mousePosition.x, -xLim, xLim ), 3f, Mathf.Clamp( Input.mousePosition.z, -zLim, zLim ) );

            //Create cube
            Instantiate( objToSpawn, clickPos, Quaternion.identity );

        }

	}
}
