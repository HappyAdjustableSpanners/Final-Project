using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotScript : MonoBehaviour {

    //State machine
    public enum State {  MovingToObject, RotateAroundObject, MoveInAndOut, EmotiveExpression }
    public State currentState = State.MovingToObject;

    //List of interesting objects
    public GameObject[] interestingObjects;
    private Transform target;
    private int currentObject;
    private bool hasObservedObject;

    //Speed
    public float moveSpeed = 0.5f;

    //Components
    private Rigidbody rb;


	// Use this for initialization
	void Start () {
        //Initialise interesting objects
        interestingObjects = GameObject.FindGameObjectsWithTag("InterestingObject");
        currentObject = 0;

        //Initialise rigidbody
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
		 
        
        switch(currentState)
        {
            //If current State is move to object, move to current objectS
            case State.MovingToObject:

                //Only get the target once to save performance
                if( target == null)
                {
                    //Get the target and look to face target
                    target = interestingObjects[currentObject].transform;
                   
                }

                transform.LookAt(target.position);

                /*
                 * Lerp to target
                 * When we are close enough stop lerping
                 */
                if (Vector3.Distance(transform.position, target.position) > 0.6f)
                {
                    transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
                                       
                }
                else
                {
                    if (!hasObservedObject)
                    {
                        //Delay for observation time
                        StartCoroutine("ObserveObject");
                        hasObservedObject = true;
                    }
                }
                break;
        } 
	}

    IEnumerator ObserveObject()
    {
        yield return new WaitForSeconds(5f);

        if (currentObject < interestingObjects.Length - 1)
        {
            currentObject++;
        }
        else
            currentObject = 0;

        target = null;
        hasObservedObject = false;
    }
}
