using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorBehaviour : MonoBehaviour {

    //When game ball collides with floor, destroy it and reset the level

    //Level manager
    private PipeGameManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();
	}
	
	void OnCollisionEnter(Collision col)
	{
        //When the game ball collides with the floor, destroy it, and reset the level
        if (col.gameObject.CompareTag("GameBall")) 
        {
            //Get game ball behaviour script
            GameBallBehaviour gameBallBehaviour = col.gameObject.GetComponent<GameBallBehaviour>();

            gameBallBehaviour.setDissolve(true);

            levelManager.Reset();
        }
	}
}
