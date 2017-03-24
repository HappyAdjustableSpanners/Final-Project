using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhistleBehaviour : MonoBehaviour {

    //Wolf fetch behaviour script
    public FetchBehaviour wolfFetchBehaviour;
    
    //Audio
    private AudioSource audioSource;

    //If the player hits the whistle button while the wolf is waiting, set the wolf to delivering to player
    public void Whistle()
    {
        if (wolfFetchBehaviour.getWaitingForPlayer())
        {
            wolfFetchBehaviour.setDeliveringToPlayer(true);
        }
    }
}
