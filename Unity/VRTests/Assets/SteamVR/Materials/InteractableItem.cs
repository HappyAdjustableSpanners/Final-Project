using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {

	protected Rigidbody rigidBody;

	private Vector3 positionDelta;
	private float velocityFactor = 20000f;


	private Quaternion rotationDelta;
	private float rotationFactor = 600f;

	private float angle;
	private Vector3 axis;


	protected bool isInteracting;

	private WandController attachedWand;
	private Transform interactionPoint;

    // Use this for initialization
    protected void Awake () {
		rigidBody = GetComponent<Rigidbody> ();
		interactionPoint = new GameObject ().transform;
		velocityFactor = velocityFactor / rigidBody.mass;
		rotationFactor = rotationFactor / rigidBody.mass;
	}
	
	// Update is called once per frame
	protected void Update () {
		if (attachedWand && isInteracting) {
			positionDelta = attachedWand.transform.position - interactionPoint.position;
			rigidBody.velocity = positionDelta * velocityFactor * Time.fixedDeltaTime;

			rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse (interactionPoint.rotation);
			rotationDelta.ToAngleAxis( out angle, out axis);

			if (angle > 180) {
				angle -= 360;
			}

			rigidBody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
		}
	}

	public void BeginInteraction (WandController wand)
	{
		attachedWand = wand;
		interactionPoint.position = wand.transform.position;
		interactionPoint.rotation = wand.transform.rotation;
		interactionPoint.SetParent (transform, true);
		isInteracting = true;
	}

	public void EndInteraction (WandController wand)
	{
		if (wand == attachedWand) {
			attachedWand = null;
			isInteracting = false;
		}
	}

	public bool IsInteracting()
	{
		return isInteracting;
	}

	private void OnDestroy()
	{
		Destroy (interactionPoint.gameObject);
	}
}
