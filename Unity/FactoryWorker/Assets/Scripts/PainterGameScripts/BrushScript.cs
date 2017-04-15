using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushScript : MonoBehaviour {

    //On brush collision with paint pot, move material of paint can onto brush tip

    //Brush tip
    public GameObject brushTip;

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
