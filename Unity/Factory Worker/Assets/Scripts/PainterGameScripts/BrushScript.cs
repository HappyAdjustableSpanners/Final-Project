using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushScript : MonoBehaviour {

    //Sphere collider on end of brush
    //On Collision, move material of paint can onto brush tip

    private SphereCollider sphereCollider;
    public GameObject brushTip;

	// Use this for initialization
	void Start () {

        //get sphere collider
        sphereCollider = GetComponent<SphereCollider>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider col)
    {
        //Check if collider is paint pot paint
        if (col.CompareTag("Pot Paint"))
        {
            //Apply material of pot paint to brush tip
            Material paintMat = col.gameObject.GetComponent<Renderer>().material;
            brushTip.GetComponent<Renderer>().material = paintMat;
        }
    }
}
