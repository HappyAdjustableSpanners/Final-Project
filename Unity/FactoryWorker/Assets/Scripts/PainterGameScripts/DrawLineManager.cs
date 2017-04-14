using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

    private ControllerInputManager controllerInput;
    public Valve.VR.InteractionSystem.Hand controller;
    public Material mat;
    public GameObject brushTip;
    public GameObject brush;
    public Transform brushPaintSpawnLocation;

    private MeshLineRenderer line;
    private MeshCollider mc;
    private int numClicks = 0;
    private float lineWidth = 0.01f;
    private FadeInOutTextMesh[] textMeshFadeScripts;
    private FadeScript arrowFadeScript;
    private float brushWidthChangeSpeed = 0.0025f;
    private float brushWidthMin = 0.005f;
    private float brushWidthMax = 0.06f;

    // Use this for initialization
    void Start () {
        //Get controller input script
        controllerInput = controller.GetComponent<ControllerInputManager>();

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
                line.AddPoint(brushPaintSpawnLocation.position);
                                            
                //Increment numclicks
                numClicks++;
            }             
        }

        if (controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger))
        {
            //Update the convex mesh when we release trigger and line is complete
            mc.convex = true;

            //One in 3 change of the following happening
            int randomNum = Random.Range(0, 3);
            if (randomNum == 0)
            {
                //Create child with tag "interestingObject" so the mascot can find it
                //We parent it to the line so that when the line is erased the mascot will not come back to look at it
                GameObject obj = new GameObject();
                obj.transform.position = brushPaintSpawnLocation.position;
                obj.transform.parent = line.transform;
                obj.tag = "InterestingObject";
            }

            if (line)
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
