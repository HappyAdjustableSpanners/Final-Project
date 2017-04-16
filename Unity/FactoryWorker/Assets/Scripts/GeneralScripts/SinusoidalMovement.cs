using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMovement : MonoBehaviour {

    public float horizontalMovement = 10.0f;
    public float verticalMovement = 5.0f;
    public float speed = 1f;

    private float index;

    public enum Axis { horizontal, vertical };
    public Axis axis = Axis.vertical;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Sine wave movement
        index += Time.deltaTime;
        float x = horizontalMovement * Mathf.Cos(speed * index);
        float y = verticalMovement * Mathf.Sin(speed * index);

        if (axis == Axis.vertical)
        {
            //Update the local position (relative to its parent)
            transform.localPosition = new Vector3(0, y, x);
        }
        else
            transform.localPosition = new Vector3(x, transform.localPosition.y, y);
    }
}
