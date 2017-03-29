using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCameraScript : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {   
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        transform.position += movement * speed * Time.deltaTime; 
	}
}
