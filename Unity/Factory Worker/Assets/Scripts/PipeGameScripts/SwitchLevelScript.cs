using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevelScript : MonoBehaviour {

    private PipeGameManager pipeGameManager;

	// Use this for initialization
	void Start () {
        pipeGameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Controller"))
        {
            if (this.CompareTag("NextLevelButton"))
            {
                pipeGameManager.LoadNextScene();
            }
            else if (this.CompareTag("PrevLevelButton"))
            {
                pipeGameManager.LoadPrevScene();
            }
        }
    }
}
