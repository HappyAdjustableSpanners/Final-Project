using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryTargetBehaviour : MonoBehaviour {

    //Movement values and speed
    public float horizontalMovement = 10.0f;
    public float verticalMovement = 5.0f;
    public float speed = 1f;

    //Sinusoidal movement
    private float index;

    //Reference to game controller
    private ArcheryGameManager gameController;

    // Use this for initialization
    void Start () {
        //Get game controller
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<ArcheryGameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        //Sine wave movement
        index += Time.deltaTime;
        float x = horizontalMovement * Mathf.Cos(speed * index);
        float y = verticalMovement * Mathf.Sin(speed * index);

        //Update the local position (relative to its parent)
        transform.localPosition = new Vector3(0, y, x);

    }

    void OnCollisionEnter(Collision col)
    {
        //If we hit a target, call the next target method from the game controller
        if (col.gameObject.name == "Arrowhead collider")
        {
            gameController.TargetHit();
        }
    }
}
