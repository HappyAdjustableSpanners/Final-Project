﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickScript : MonoBehaviour {

    //Get stick rigidbody
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
    //Sets and gets
    public void setUseGravity(bool val)
    {
        rb.useGravity = val;
    }
}
