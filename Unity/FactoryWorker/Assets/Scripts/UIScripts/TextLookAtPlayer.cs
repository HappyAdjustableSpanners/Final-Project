using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAtPlayer : MonoBehaviour {

    public Transform playerPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(playerPos.position, Vector3.up);	
	}
}
