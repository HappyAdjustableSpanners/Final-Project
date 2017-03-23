using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GrabSphereBehaviour : MonoBehaviour {

    private InteractableItem interactableItemBehaviour;
    private Interactable interactableBehaviour;
    private Renderer rend;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float fadeSpeed = 3f;
    private bool isGrabbed = false;
    private AudioSource audioSource;
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

        if(rend.material.GetFloat("_SliceAmount") > 0.85f && !loadingLevel)
        {
            loadingLevel = true;
            audioSource.Play();
            SteamVR_LoadLevel.Begin(this.tag, false, 0.5f, 0f, 0f, 0f, 1f);
        }
    }

    public void setGrabbed(bool value)
    {
        isGrabbed = value;
    }
}
