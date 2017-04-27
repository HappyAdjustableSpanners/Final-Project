using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotScript : MonoBehaviour {

    //State machine
    public enum State {  MovingToObject, RotateAroundObject, MoveInAndOut, EmotiveExpression }
    public State currentState = State.MovingToObject;

    //List of interesting objects
    private GameObject[] interestingObjects;
    private List<GameObject> interestingObjectsList = new List<GameObject>();
    private Transform target;
    private int currentObject;
    private bool haveObservedObject;
    public float observationDelay = 5f;

    //Movement
    public float moveSpeed = 0.5f;
    public float decelerationSpeed = 6f;
    private Vector3 desiredVelocity;
    public float turnSpeed = 5f;

    //Rigidbody
    private Rigidbody rb;

	// Use this for initialization
	void Start () {

        InvokeRepeating("GetInterestingObjects", 0f, 10f);
        //Initialise interesting objects
        currentObject = 0;

        //Initialise rigidbody
        rb = GetComponent<Rigidbody>();

    }

    void GetInterestingObjects()
    {
        //Empty the interesting obects list
        interestingObjectsList.Clear();

        interestingObjects = GameObject.FindGameObjectsWithTag("InterestingObject");

        for(int i = 0; i < interestingObjects.Length; i++)
        {
            interestingObjectsList.Add(interestingObjects[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            //If current State is move to object, move to current objectS
            case State.MovingToObject:
            {

                    //If the element is not null
                    if (currentObject < interestingObjectsList.Count)
                    {
                        if (interestingObjectsList[currentObject] != null)
                        {
                            target = interestingObjectsList[currentObject].transform;
                        }
                        else
                        {
                            interestingObjectsList.RemoveAt(currentObject);
                            break;
                        }
                    }
                    else
                        currentObject = 0;

                    if (target)
                    {

                        //Calculate desired velocity
                        desiredVelocity = CalculateDesiredVelocity();

                        // Turn to face
                        //Get look at rotation
                        Quaternion lookAt = Quaternion.LookRotation(desiredVelocity);
                        transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, Time.deltaTime * turnSpeed);

                        /*
                         * Lerp to target
                         * When we are close enough stop lerping
                         */
                        if (Vector3.Distance(rb.position, target.position) > 0.5f)
                        {
                            //Apply the velocity
                            rb.velocity = desiredVelocity;
                        }
                        else
                        {
                            //Slow to a stop
                            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * decelerationSpeed);

                            //Lerp to face target
                            lookAt = Quaternion.LookRotation(target.position - transform.position);
                            transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, Time.deltaTime * turnSpeed);

                            if (!haveObservedObject)
                            {
                                //Delay for observation time  
                                StartCoroutine("ObserveObject");
                                haveObservedObject = true;
                            }
                        }
                    }
                break;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (!col.CompareTag("Bounds") && !col.CompareTag("Line"))
        {
            //Get heading
            Vector3 heading = col.gameObject.transform.position - transform.position;

            //Normalise
            Vector3 normalisedHeading = heading.normalized;

            //Apply reverse
            rb.velocity -= normalisedHeading * 0.005f;
        }
    }

    private Vector3 CalculateDesiredVelocity()
    {
        //Work out the direction
        Vector3 dir = target.position - rb.position;
        
        //Normalise it and apply speed
        Vector3 velocity = dir.normalized * moveSpeed;

        return velocity;
    }

    IEnumerator ObserveObject()
    {
        yield return new WaitForSeconds(observationDelay);

        if (currentObject < interestingObjects.Length - 1)
        {
            currentObject++;
        }
        else
            currentObject = 0;

        haveObservedObject = false;
    }
}
