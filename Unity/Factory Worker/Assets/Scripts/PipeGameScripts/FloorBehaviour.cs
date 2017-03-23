using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloorBehaviour : MonoBehaviour {

    private PipeGameManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<PipeGameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
        if (col.gameObject.CompareTag("GameBall")) 
        {
            Destroy(col.gameObject);
            levelManager.Reset();
        }
	}
}
