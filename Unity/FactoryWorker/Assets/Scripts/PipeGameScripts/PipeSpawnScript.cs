using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour {

    //Object to spawn (in our case, the game ball)
    public GameObject Object;

    //The spawn position
    public Transform position;

    //Audio
    private AudioSource audioSource;
    private AudioClip audioClip;

	// Use this for initialization
	void Start () {
        //Load audio clip from resources
        audioClip = Resources.Load<AudioClip>("Audio/depressure");
    }

    public void SpawnGameObject()
    {

        //Get audio source
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        audioSource.clip = audioClip;

        //Instantiate the object to spawn
        Instantiate(Object, position.position, position.rotation);

        //Load the unlockclip into the audiosource and play it
        audioSource.Play();
    }
}
