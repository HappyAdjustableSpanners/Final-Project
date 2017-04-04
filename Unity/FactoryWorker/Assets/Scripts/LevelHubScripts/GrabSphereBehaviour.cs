using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrabSphereBehaviour : MonoBehaviour {

	//The renderer component
    private Renderer rend;

	//Original position and rotation vectors
    private Vector3 originalPosition;
    private Quaternion originalRotation;

	//Speed at which the sphere fades
    private float fadeSpeed = 3f;

	//Whether the sphere is being grabbed
    private bool isGrabbed = false;

	//Audio
    private AudioSource audioSource;

	//Makes sure we do not load level multiple times
    private bool loadingLevel = false;

    // Use this for initialization
    void Start () { 
       
        //Get reference to our renderer
        rend = GetComponent<Renderer>();

        //Get reference to original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        //Get audio source
        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

        //if the sphere is being held, fade it out by increasing the slice amount of the material
        if (isGrabbed)
        {
            rend.material.SetFloat("_SliceAmount", Mathf.Lerp(rend.material.GetFloat("_SliceAmount"), 1, Time.deltaTime * fadeSpeed));
        }
        else
        {
            //If the material is released, revert the slice and return the sphere to it's original position
            rend.material.SetFloat("_SliceAmount", Mathf.Lerp(rend.material.GetFloat("_SliceAmount"), 0, Time.deltaTime * fadeSpeed));
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }

		//If the slice amount is close to complete slice, load the level
        if(rend.material.GetFloat("_SliceAmount") > 0.85f && !loadingLevel)
        {
            //Set loading level to true
            loadingLevel = true;

            //Play the level change sound effect
            audioSource.Play();

            //Stop the current music
            MusicPlayer.StopMusic();

            //If we are not going to the pipe game, carry on
            if (!this.CompareTag("PipeGame1"))
            {
                SteamVR_LoadLevel.Begin(this.tag, false, 0.5f, 0f, 0f, 0f, 1f);
            }
            else
            {
                //Else, we need to get the top level of the pipe game and load that
                string sceneName = "PipeGame" + PipeGameManager.getTopLevel();
                SteamVR_LoadLevel.Begin(sceneName, false, 0.5f, 0f, 0f, 0f, 1f);
            }
        }
    }

	//Gets and sets
    public void setGrabbed(bool value)
    {
        isGrabbed = value;
    }
}
