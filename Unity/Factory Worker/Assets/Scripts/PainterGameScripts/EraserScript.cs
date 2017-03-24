using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserScript : MonoBehaviour {

    public Valve.VR.InteractionSystem.HandPainter hand2;

	// Use this for initialization
	void Start () {
	    	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if(hand2.getTriggerHeld() && col.gameObject.CompareTag("Line") )
        {
            Destroy(col.gameObject);
        }
    }
}
