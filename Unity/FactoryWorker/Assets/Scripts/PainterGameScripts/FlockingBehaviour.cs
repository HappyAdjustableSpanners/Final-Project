using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlockingBehaviour : MonoBehaviour {

	private HashSet<GameObject> neighbours = new HashSet<GameObject>();
	private Vector3 v1, v2, v3, v4;
	private float w1, w2, w3, w4;
	private Quaternion heading;
	private float smoothing;
	private BoxCollider bounds;
	private Rigidbody rb;
	private Vector3 turnPoint;
	private bool boundingOn = false;

	public float flockingDensity = 20f;
	public float speed = 1f;

	private bool negateCohesion;
	private bool negateAlignment;
	private bool negateSeparation;
    private GameObject gameController;

	// Use this for initialization
	void Awake () {

        //Get settings from Game controller
        gameController = GameObject.FindGameObjectWithTag("GameController");
		boundingOn = gameController.GetComponent<BoidBehaviours> ().GetBoundingOn();
		bounds = GameObject.FindGameObjectWithTag ("Bounds").GetComponent<BoxCollider>();

        negateCohesion = gameController.GetComponent<BoidBehaviours>().getNegateCohesion();
        negateSeparation = gameController.GetComponent<BoidBehaviours>().getNegateSeparation();
        negateAlignment = gameController.GetComponent<BoidBehaviours>().getNegateAlignment();

        smoothing = 0.2f;

		rb = GetComponent<Rigidbody> ();

		//Set up weightings for each rule
		w1 = 0.3f;
		w2 = 0.1f;
		w3 = 0.8f;
		w4 = 0.3f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

			//Move randomly but obey Rule4 (Bounding Rule)
			Vector3 originalVelocity = Random.insideUnitSphere * smoothing;
	
			if (!negateCohesion) {
				v1 = Rule1 (); //cohesion
			}
			if (!negateSeparation) {
				v2 = Rule2 (); //Separation
			}
			if (!negateAlignment) {
				v3 = Rule3 (); //Alignment
			}

			if (boundingOn) {
				v4 = Rule4 (); //Bounding
			}

			//Apply weightings
			v1 *= w1;
			v2 *= w2;
			v3 *= w3;
			v4 *= w4;

			//Update velocity
			rb.velocity += originalVelocity + ((v1 + v2 + v3 + v4));

			//Clamp velocity
			rb.velocity = Vector3.ClampMagnitude (rb.velocity, 2f);

			//Look the way we are going
			transform.forward = Vector3.Slerp (transform.forward, rb.velocity, Time.deltaTime * speed);
	}
		
	private Vector3 Rule4()
	{
		Vector3 v = Vector3.zero;

			if (transform.position.x < bounds.bounds.min.x) {
				v.x = 0.1f;			
			} else if (transform.position.x > bounds.bounds.max.x) {
				v.x = -0.1f;
			}

			if (transform.position.y < bounds.bounds.min.y) {
				v.y = 0.1f;			
			} else if (transform.position.y > bounds.bounds.max.y) {
				v.y = -0.1f;
			}

			if (transform.position.z < bounds.bounds.min.z) {
				v.z = 0.1f;			
			} else if (transform.position.z > bounds.bounds.max.z) {
				v.z = -0.1f;
			}

			return v;
		
	}

	void OnTriggerEnter(Collider col)
	{
		//if (!neighbours.Contains (col.gameObject)) {
		if (col.tag == "Boid") {
			neighbours.Add (col.gameObject);
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Boid") {
			neighbours.Remove (col.gameObject);
		}
	}

	private Vector3 Rule1()
	{
		//Boids try to fly towards the centre of mass of neighbouring boids
		Vector3 perceivedCenter = Vector3.zero;
		Vector3 movement;

		//For each neighbour, add up all their velocities and find the average
		foreach (GameObject b in neighbours) {
			GameObject boid = GameObject.Find (b.name);
			perceivedCenter += boid.transform.position;
		}


		//Find the average. We -1 to numBoids here as we don't want to include this boid in the average
		perceivedCenter = perceivedCenter / (neighbours.Count);

		//Move the boid towards the perceived center
		if (perceivedCenter != Vector3.zero) {
			movement = perceivedCenter - transform.position;
		} else
			movement = Vector3.zero;

		//Return the movement
		return movement;
	}

	private Vector3 Rule2()
	{
		//Boids try to keep a small distance away from other boids. Seperation
		Vector3 movement = Vector3.zero;

		//For each neighbour, subtract the displacement from that neighbour to ourselves
		foreach (GameObject b in neighbours) {
			
			GameObject boid = GameObject.Find (b.name);

			if (Vector3.Distance (transform.position, boid.transform.position) < flockingDensity) 
			{
				movement = movement - (boid.transform.position - transform.position);
			}
		}

		return movement;
	}

	private Vector3 Rule3()
	{
		//Boids try to match velocity with near boids - Alignment

		//Boids try to match their velocity to the avg velocity of neighbouring boids
		Vector3 perceivedAvgVel = Vector3.zero;
		Vector3 movement;

		//For each neighbour, add up all their velocities and find the average
		foreach (GameObject b in neighbours) 
		{
			FlockingBehaviour boid = GameObject.Find (b.name).GetComponent<FlockingBehaviour>();

			perceivedAvgVel += boid.getVelocity();
		}

		//Find the average. We -1 to numBoids here as we don't want to include this boid in the average
		perceivedAvgVel = perceivedAvgVel / (neighbours.Count);

		//Move the boid towards the perceived center
		if (perceivedAvgVel != Vector3.zero) {
			movement = perceivedAvgVel - getVelocity ();
		} else
			movement = Vector3.zero;

		//Return the movement
		return movement;
	}

	public Vector3 getVelocity()
	{
		return rb.velocity;
	}
}
