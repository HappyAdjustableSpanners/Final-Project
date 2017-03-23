using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleBehaviour : MonoBehaviour {

    public FetchBehaviour wolfFetchBehaviour;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    public void Whistle()
    {
        if (wolfFetchBehaviour.isWaitingForPlayer())
        {
            wolfFetchBehaviour.setDeliveringToPlayer(true);
        }
        
        //Play whistle sound

    }
}
