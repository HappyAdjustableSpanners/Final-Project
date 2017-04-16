using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    //Green and white materials
    private Material greenMat, whiteMat;

    //Level manager
    private PipeGameManager levelManager;

    //Has the platform been hit?
    private bool platformReady = false;

    //Audio
    private AudioSource audioSource;
    private AudioClip audioClip;

    // Use this for initialization
    void Start () {

        //Load the materials from resources
        greenMat = Resources.Load("Materials/PlainColors/M_Green", typeof(Material)) as Material;
        whiteMat = Resources.Load("Materials/PlainColors/M_White", typeof(Material)) as Material;

        //Get the level manager
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();

        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/win_beep");
        audioSource.clip = audioClip;
        audioSource.volume = 0.3f;
    }
	
    void OnCollisionEnter(Collision col)
    {
        //If the platform has not already been hit
        if (!platformReady)
        { 
            //If the collider is the game ball
            if (col.gameObject.CompareTag("GameBall"))
            {
                //Change the color of the platform to green
                GetComponent<Renderer>().material = greenMat;

                //Signal to the level manager that a platform is ready
                levelManager.PlatformReady();
                platformReady = true;

                //Play the audio source
                audioSource.Play();
            }
        }
    }

    public void Reset()
    {
        //Change the color of the platform back to white
        GetComponent<Renderer>().material = whiteMat;
        platformReady = false;
    }
}
