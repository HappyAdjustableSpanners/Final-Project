using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    public ControllerInputManager controllerInput;
    public Valve.VR.InteractionSystem.HandPainter controller;
    public Material mat;
    public GameObject brushTip;
    public GameObject brush;


    private MeshLineRenderer line;
    private MeshCollider mc;
    private int numClicks = 0;
    private float lineWidth = 0.01f;
    private FadeInOutTextMesh[] textMeshFadeScripts;
    private FadeScript arrowFadeScript;
    public Transform brushTipTip;
    private float brushWidthChangeSpeed = 0.0025f;
    private float brushWidthMin = 0.005f;
    private float brushWidthMax = 0.06f;

    public GameObject hand1;

    // Use this for initialization
    void Start () {
        //Get text scripts
        arrowFadeScript = GameObject.Find("UI/GameHints/TextMesh/arrow").GetComponent<FadeScript>();
        textMeshFadeScripts = GameObject.Find("UI/GameHints").GetComponentsInChildren<FadeInOutTextMesh>();
	}
	
	// Update is called once per frame
	void Update () {

        //If the brush hand trigger is pressed, draw a line
        if (controllerInput.triggerDown)
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
            line.setWidth(lineWidth);
                
            //Init num clicks to 0
            numClicks = 0;
        }
        else if(controllerInput.triggerTouch)
        {
            if (line != null)
            {
                //Set width of the line
                line.setWidth(lineWidth);

                //While the trigger is held, keep adding points to the line
                line.AddPoint(brushTipTip.position);

                //Increment numclicks
                numClicks++;
            }             
        }

        if (controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            mc.convex = true;

            if(line)
            {
                line = null;
            }
        }

        if ( controllerInput.dPadLeftTouch )
        {
            DecrementBrushWidth();
        }

        if( controllerInput.dPadRightTouch )
        {
            IncrementBrushWidth();
        }      
    }

    public void IncrementBrushWidth()
    {
        //If the new size is within the limits
        if (lineWidth < brushWidthMax )
        {
            //Set brush width
            lineWidth += brushWidthChangeSpeed;

            //Get current size
            float currentSize = brush.transform.localScale.x;

            //Get new size
            float newSize = currentSize + 0.03f;

            brush.transform.localScale = new Vector3(newSize, brush.transform.localScale.y, newSize);
        }

        //Fade out text hint if it is faded in
        if( textMeshFadeScripts[0].getFade() )
        {
            foreach(FadeInOutTextMesh textMeshFadeScript in textMeshFadeScripts)
            {
                textMeshFadeScript.FadeOut();
                arrowFadeScript.FadeOut();
            }
        }
    }

    public void DecrementBrushWidth()
    {
        //If the new size is within the limits
        if (lineWidth > brushWidthMin)
        {
            //Set brush width
            lineWidth -= brushWidthChangeSpeed;

            //Get current size
            float currentSize = brush.transform.localScale.x;

            //Get new size
            float newSize = currentSize - 0.03f;

            brush.transform.localScale = new Vector3(newSize, brush.transform.localScale.y, newSize);
        }
    }
}
