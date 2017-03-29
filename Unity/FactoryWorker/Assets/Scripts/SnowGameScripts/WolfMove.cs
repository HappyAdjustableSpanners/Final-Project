using UnityEngine;
using System.Collections;

public class WolfMove : MonoBehaviour {

	public float moveSpeed = 2f;
	private Rigidbody rb;

	public bool wander = false;

	//Turning
	private float turnTimer;
	private Quaternion turnRotation;
	private float turnDirection;
	public float turnFrequency = 1f;
	public float turnSpeed = 1f;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		//Turn parameters
		turnDirection = Random.Range (0f, 360f);
		turnRotation = Quaternion.Euler (0f, turnDirection, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
		rb.MovePosition (transform.position + movement);

		if (wander)
		{
			//Choose a direction, create a Quaternion rotation, apply the rotation
			//choose a new rotation every second

			turnTimer += Time.deltaTime;

			if (turnTimer >= turnFrequency) {
				turnDirection = Random.Range (0f, 360f);
				turnRotation = Quaternion.Euler (0f, turnDirection, 0f);
				turnTimer = 0f;
			}
			rb.MoveRotation ( Quaternion.Lerp( transform.rotation, turnRotation, Time.deltaTime * turnSpeed));
		}
	}
}
