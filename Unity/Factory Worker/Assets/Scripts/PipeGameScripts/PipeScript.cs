using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PipeScript : MonoBehaviour {

    private PipeGameManager levelManager;
    private AudioSource audioSource;
    private AudioClip audioClip;
    public Renderer pipeBodyRenderer;

    void Start()
    { 
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();

        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/win_beep2");
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("GameBall"))
        {
            pipeBodyRenderer.materials[1].color = Color.green;
            levelManager.PipeReady();
            //levelManager.SpawnGameBall();
            Destroy(col.gameObject);

            //Get audio source
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
            levelManager.CheckFinished();
        }
    }

    public void Reset()
    {
        pipeBodyRenderer.materials[1].color = Color.red;
    }
}
