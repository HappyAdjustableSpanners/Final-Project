using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

	public GameObject head_bone;
	public GameObject main_camera;

	private float origX, origY, origZ;

	// Use this for initialization
	void Start () {

		//Record the originial rotations (needed for clamps)
		origX = head_bone.transform.localEulerAngles.x;
		origY = head_bone.transform.localEulerAngles.y;
		origZ = head_bone.transform.localEulerAngles.z;
	
	}
	
	// Update is called once per frame
	void Update () {

		//Look at the main camera
		head_bone.transform.LookAt ( main_camera.transform.position, new Vector3(0f, 0f, 1f));

		//Clamp the rotation
		head_bone.transform.localEulerAngles = new Vector3( ClampAngle (head_bone.transform.localEulerAngles.x, origX - 100, origX + 100),
													   ClampAngle (head_bone.transform.localEulerAngles.y, origY -20, origY + 20), 
													   ClampAngle( head_bone.transform.localEulerAngles.z, origZ -10, origZ + 10));

	}

	private float ClampAngle(float angle, float min, float max) {

		if (angle<90 || angle>270){       // if angle in the critic region...
			if (angle>180) angle -= 360;  // convert all angles to -180..+180
			if (max>180) max -= 360;
			if (min>180) min -= 360;
		}    
		angle = Mathf.Clamp(angle, min, max);
		if (angle<0) angle += 360;  // if angle negative, convert to 0..360
		return angle;
	}
}
