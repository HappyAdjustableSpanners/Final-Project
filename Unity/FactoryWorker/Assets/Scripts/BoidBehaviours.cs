using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidBehaviours : MonoBehaviour {

	private Vector3 velocity;
	private Quaternion rotation;
	private float speed;
	private Vector3 v1, v2, v3;
	private BoxCollider bounds;

	public int numBoids;
	public GameObject boid;
	public bool boundingOn;

	public bool negateCohesion;
	public bool negateAlignment;
	public bool negateSeparation;

	// Use this for initialization
	void Start () {

		//Initialise bounds
		bounds = GameObject.FindGameObjectWithTag("Bounds").GetComponent<BoxCollider>();

	
		for (int i = 0; i < numBoids; i++) {
			//Get random position
			float x = Random.Range (-bounds.bounds.extents.x, bounds.bounds.extents.x);
			float y = Random.Range (-bounds.bounds.extents.y, bounds.bounds.extents.y);
			float z = Random.Range (-bounds.bounds.extents.z, bounds.bounds.extents.z);

            //Give the boid a unique name
            if (boid != null)
            {
                boid.name = "Boid" + i;
            }

            //Instantiate the boid
			GameObject newBoid = Instantiate (boid, new Vector3 (x, y, z), Random.rotation);

            //Give it a random size
            float randSize = Random.Range(0.5f, 5f);
            boid.transform.localScale = new Vector3(randSize, randSize, randSize);
		}
	}

	// Update is called once per frame
	void Update () {
	}

	public int GetNumBoids()
	{
		return numBoids;
	}

	public bool GetBoundingOn()
	{
		return boundingOn;
	}

	public bool getNegateCohesion()
	{
		return negateCohesion;
	}

	public bool getNegateSeparation()
	{
		return negateSeparation;
	}

	public bool getNegateAlignment()
	{
		return negateAlignment;
	}
}
