using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour {

    //Selectable axis for rotation
    public enum Axis { X, Y, Z };
    public Axis axis = Axis.X;

    //Speed of rotation
    public float speed = 1f;
	
	// Update is called once per frame
	void Update () {
		
        //Rotate around the specified axis
        switch (axis)
        {
            case Axis.X:
                transform.Rotate(Vector3.left, speed * Time.deltaTime);
                break;
            case Axis.Y:
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
                break;
            case Axis.Z:
                transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                break;
        }
	}
}
