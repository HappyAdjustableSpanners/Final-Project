using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    //Green and white materials
    private Material greenMat, pinkMat;

    //Level manager
    private PipeGameManager levelManager;

    //Has the platform been hit?
    private bool obstacleReady = false;

    //Audio
    private AudioSource audioSource;
    private AudioClip audioClip;

    //Renderer
    private Renderer rend;

    // Use this for initialization
    void Start()
    {
        //Load the materials from resources
        greenMat = Resources.Load("Materials/M_Green", typeof(Material)) as Material;
        pinkMat = Resources.Load("Materials/M_Pink", typeof(Material)) as Material;

        //Get the level manager
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();

        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/win_beep");
        audioSource.clip = audioClip;

        //get renderer
        rend = GetComponentInChildren<Renderer>();
        rend.material = pinkMat;
    }

    void OnTriggerEnter(Collider col)
    {
        //If the platform has not already been hit
        if (!obstacleReady)
        {
            //If the collider is the game ball
            if (col.gameObject.CompareTag("GameBall"))
            {
                //Change the color of the platform to green
                rend.material = greenMat;

                //Signal to the level manager that a platform is ready
                levelManager.ObstacleReady();
                obstacleReady = true;

                //Play the audio source
                audioSource.Play();
            }
        }
    }

    public void Reset()
    {
        //Change the color of the platform back to white
        GetComponent<Renderer>().material = pinkMat;
        obstacleReady = false;
    }
}
