using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGameController : MonoBehaviour {

    //get wolf fetch behaviour
    public FetchBehaviour wolfFetchBehaviour;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


    }

    public void PlayerWhistle()
    {
        if( wolfFetchBehaviour.isWaitingForPlayer() )
        {
            wolfFetchBehaviour.setDeliveringToPlayer(true);
        }


    }
}
