using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevelScript : MonoBehaviour {

    //Level manager
    private PipeGameManager pipeGameManager;

	// Use this for initialization
	void Start () {
        //Get the level manager
        pipeGameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();	
	}

    private void OnCollisionStay(Collision col)
    {
        //If the collider is the controller
        if (col.gameObject.CompareTag("Controller"))
        {
            if (this.CompareTag("NextLevelButton"))
            {
                //If the collider is the controller, and we are the next level button, load the next scene
                pipeGameManager.LoadNextScene();
            }
            else if (this.CompareTag("PrevLevelButton"))
            {
                //If the collider is the controller, and we are the previous level button, load the next scene
                pipeGameManager.LoadPrevScene();
            }
        }
    }
}
