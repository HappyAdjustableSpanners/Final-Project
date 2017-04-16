using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLookAtPlayer : MonoBehaviour {

    public Transform playerPos;

	// Use this for initialization
	void Start () {
        //playerPos = GameObject.Find("Player").transform.Find("SteamVRObjects/VRCamera/FollowHead");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(playerPos.position, Vector3.up);	
	}
}
