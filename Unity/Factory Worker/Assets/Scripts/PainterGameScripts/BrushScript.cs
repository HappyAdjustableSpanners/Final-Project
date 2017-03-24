using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushScript : MonoBehaviour {

    //On brush collision with paint pot, move material of paint can onto brush tip

    //Sphere collider on end of brush
    private SphereCollider sphereCollider;

    //Brush tip
    public GameObject brushTip;

	// Use this for initialization
	void Start () {
        //get sphere collider
        sphereCollider = GetComponent<SphereCollider>();
	}

    public void OnTriggerEnter(Collider col)
    {
        //Check if collider is paint pot paint
        if (col.CompareTag("Pot Paint"))
        {
            //Apply material of pot paint to brush tip via renderer
            Material paintMat = col.gameObject.GetComponent<Renderer>().material;
            brushTip.GetComponent<Renderer>().material = paintMat;
        }
    }
}
