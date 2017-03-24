using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PipeScript : MonoBehaviour {

    //Level manager
    private PipeGameManager levelManager;

    //Audio
    private AudioSource audioSource;
    private AudioClip audioClip;

    //Has the pipe been hit?
    private bool pipeReady = false;

    //Renderer of pipe
    public Renderer pipeBodyRenderer;

    void Start()
    { 
        //Get level manager
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();

        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/win_beep2");
    }
    void OnTriggerEnter(Collider col)
    {
        if (!pipeReady)
        {
            //If the game ball has hit the collider
            if (col.CompareTag("GameBall"))
            {
                //Change the pipe color to green
                pipeBodyRenderer.materials[1].color = Color.green;

                //Signal to the level manager that a pipe is ready
                levelManager.PipeReady();

                //Destroy the game ball
                Destroy(col.gameObject);

                //Get audio source, load the clip and play it
                audioSource = GetComponent<AudioSource>();
                audioSource.clip = audioClip;
                audioSource.Play();

                pipeReady = true;

                //Check if all pipes have been hit
                levelManager.CheckFinished();
            }
        }
    }

    public void Reset()
    {
        //Change the pipe color back to red
        pipeBodyRenderer.materials[1].color = Color.red;
        pipeReady = false;
    }
}
