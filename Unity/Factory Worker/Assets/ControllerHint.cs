using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHint : MonoBehaviour {

    //The local position to the controller, at which the hint is to be displayed
    public Transform hintPosition;

    //Line renderer
    private LineRenderer lineRenderer;

    //Controller position
    public Vector3 lineStartPos, lineEndPos;

    //Line color
    private Color lineColor;

    public int handIndex;

    void Awake()
    {
        lineStartPos = transform.position;
        lineStartPos = new Vector3(lineStartPos.x - 0.01f, lineStartPos.y - 0.05f, lineStartPos.z);
        //Line renderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.SetPosition(0, lineStartPos);
        lineRenderer.SetPosition(1, lineEndPos);
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.001f; lineRenderer.endWidth = 0.001f;
        lineRenderer.material = Resources.Load<Material>("Materials/M_Black_Alpha_Blend");
        lineColor = new Color(0, 0, 0, 0);
        lineRenderer.startColor = lineColor; lineRenderer.endColor = lineColor;
    }
	// Use this for initialization
	void Start () {

        if (handIndex == 1)
        {
            //Set the controller as parent
            transform.SetParent(GameObject.Find("Player").transform.Find("SteamVRObjects/Hand1"));
        }
        else
        {
            //Set the controller as parent
            transform.SetParent(GameObject.Find("Player").transform.Find("SteamVRObjects/Hand2"));
        }

        //Set the transforms
        transform.localPosition = hintPosition.position;
        transform.rotation = hintPosition.rotation;
        transform.localScale = hintPosition.localScale;
    }
}
