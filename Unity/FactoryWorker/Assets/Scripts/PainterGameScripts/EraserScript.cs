using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserScript : MonoBehaviour {

    //Controller
    public ControllerInputManager controller;

    void Start()
    {
        controller = transform.parent.GetComponent<ControllerInputManager>();
    }

    //When the eraser collides with a line, remove it
    void OnTriggerStay(Collider col)
    {
        if(controller.triggerTouch && col.gameObject.CompareTag("Line") )
        {
            Destroy(col.gameObject);
        }
    }
}
