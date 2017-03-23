using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour {

    public GameObject Object;
    public Transform position;
    private AudioSource audioSource;
    private AudioClip audioClip;

	// Use this for initialization
	void Start () {

        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/depressure");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnGameObject()
    {

        //Get audio source
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;

        //Instantiate the object to spawn
        Instantiate(Object, position.position, position.rotation);

        //Load the unlockclip into the audiosource and play it
        audioSource.Play();
    }
}
