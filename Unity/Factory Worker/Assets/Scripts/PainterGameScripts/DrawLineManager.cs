using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    public Material mat;
    public GameObject brushTip;
    public GameObject brush;
    public ColorManager cm;
    public bool colorPickerMode = true;

    private MeshLineRenderer line;
    private MeshCollider mc;
    private int numClicks = 0;
    private bool triggerDown, triggerTouch;
    private float brushWidth = 0.01f;

    public GameObject hand1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //If the trigger is pressed
        if (triggerDown)
        {
            //Create the gamobject
            GameObject go = new GameObject();

            //Set the tag to "Line" so colliders know what they are interacting with
            go.tag = "Line";

            //Add a meshfilter to the line
            MeshFilter mf = go.AddComponent<MeshFilter>();

            //Add a mesh renderer to the line
            go.AddComponent<MeshRenderer>();

            //Add a mesh collider to the line (so they eraser can detect collisions)
            mc = go.AddComponent<MeshCollider>();

            //Set the mesh of the mesh collider
            mc.sharedMesh = mf.mesh;

            //Add a mesh line renderer
            line = go.AddComponent<MeshLineRenderer>();

            //Get the material of the brush tip and assign it to the line
            mat = brushTip.GetComponent<Renderer>().material;
            line.lmat = new Material(mat);

            //Set width of the line
            line.setWidth(brushWidth);
                
            //Init num clicks to 0
            numClicks = 0;
        }
        else if (triggerTouch)    //if held down
        {
            //While the trigger is held, keep adding points to the line
            line.AddPoint(hand1.transform.position);

            //Increment numclicks
            numClicks++;

            //Set convex to true. We do this every time we add a point as it updates the convex mesh
            mc.convex = true;
        }

        //If we are using the color picker, set the color of the brush instead to the color specified by our color manager
        if (colorPickerMode)
        {
            brushTip.GetComponent<Renderer>().material.color = cm.color;
        }
	}


    public void setBrushWidth(Vector2 orig_pos)
    {
        //Make a copy of the position variable
        Vector2 pos = orig_pos;

        //pos.x will now start at 0-2 instead of -1 to 1
        pos.x += 0.5f;

        //Set the brush width
        brushWidth = pos.x / 20;

        //See what the new size will be
        Vector3 newSize = new Vector3(brush.transform.localScale.x + orig_pos.x, brush.transform.localScale.y + 0, brush.transform.localScale.z + orig_pos.x);

        //If the new size is within the limits
        if (newSize.x >= 0.2 && newSize.x <= 0.7)
        {
            brush.transform.localScale = newSize;
        }
    }

    //Gets and sets
    public void setTriggerDown(bool value)
    {
        triggerDown = value;
    }

    public bool getTriggerDown()
    {
        return triggerDown;
    }

    public void setTriggerHold(bool value)
    {
        triggerTouch = value;
    }

    public bool getTriggerHold()
    {
        return triggerTouch;
    }
}
