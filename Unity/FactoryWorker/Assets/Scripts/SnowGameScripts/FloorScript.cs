using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

    //Wolf fetch behaviour script
    public FetchBehaviour wolfFetchBehaviour;

    //Set the wolf to fetch mode when the stick hits the ground
    void OnCollisionEnter(Collision col)
    {
        wolfFetchBehaviour.setFetch(true);
    }
}
