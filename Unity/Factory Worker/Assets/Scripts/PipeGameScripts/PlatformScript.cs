using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

    private Material greenMat, whiteMat;
    private PipeGameManager levelManager;
    private bool platformReady = false;
    private AudioSource audioSource;
    private AudioClip audioClip;

    // Use this for initialization
    void Start () {
        //levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<Level1Manager>();
        greenMat = Resources.Load("Materials/M_Green", typeof(Material)) as Material;
        whiteMat = Resources.Load("Materials/M_White", typeof(Material)) as Material;

        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();

        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/win_beep");
        audioSource.clip = audioClip;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        if (!platformReady)
        { 
            if (col.gameObject.CompareTag("GameBall"))
            {
                GetComponent<Renderer>().material = greenMat;
                levelManager.PlatformReady();
                platformReady = true;
                audioSource.Play();
            }
        }
    }

    public void Reset()
    {
        GetComponent<Renderer>().material = whiteMat;
        platformReady = false;
    }
}
