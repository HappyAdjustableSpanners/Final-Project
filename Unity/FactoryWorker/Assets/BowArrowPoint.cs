using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowArrowPoint : MonoBehaviour {

    //Look for target in the scene, if there is one, look at it

    //The target position
    private Transform archeryTarget;

    //Renderer
    private Renderer rend;

    //Original alpha
    private Color origColor;

	// Use this for initialization
	void Start () {

        //get renderer
        rend = GetComponentInChildren<Renderer>();

        //get original alpha 
        origColor = rend.material.GetColor("_TintColor");
    }
	
	// Update is called once per frame
	void Update () {

        //Look for target
        if (GameObject.FindGameObjectWithTag("ArcheryTarget") != null)
        {
            //Set target
            archeryTarget = GameObject.FindGameObjectWithTag("ArcheryTarget").transform;

            //Set transparancy to visible
            rend.material.SetColor("_TintColor", origColor);
        }
        else
        {
            //Set transparancy to invisible
            rend.material.SetColor("_TintColor", new Color(origColor.r, origColor.g, origColor.b, 0f));
        }


        //Look at target
        transform.LookAt(archeryTarget);	
	}
}
