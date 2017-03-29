using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

    //The camera to look at
    public GameObject target;

    //Head bone and camera
    private GameObject head_bone;

    //Look speed
    public float lookSpeed = 2f;

    //The starting angles of each axis
	private float origX, origY, origZ;

    // Use this for initialization
    void Start()
    {
        //Get head bone
        head_bone = transform.Find("Armature/Root/Body/Spine4/Spine3/Spine2/Spine1/Head").gameObject;

        //Record the originial rotations (needed for clamps)
        origX = head_bone.transform.localEulerAngles.x;
        origY = head_bone.transform.localEulerAngles.y;
        origZ = head_bone.transform.localEulerAngles.z;
    }
        
	
	// Update is called once per frame
	void Update () {

        if (target)
        {
            //Calculate the look rotation
            Quaternion lookRotation = Quaternion.LookRotation(target.transform.position - head_bone.transform.position);

            //Look at the camera
            head_bone.transform.rotation = Quaternion.Slerp(head_bone.transform.rotation, lookRotation, lookSpeed * Time.deltaTime);

            //Clamp the rotation
            head_bone.transform.localEulerAngles = new Vector3(ClampAngle(head_bone.transform.localEulerAngles.x, origX - 100, origX + 100),
                                                           ClampAngle(head_bone.transform.localEulerAngles.y, origY - 40, origY + 40),
                                                           ClampAngle(head_bone.transform.localEulerAngles.z, origZ - 10, origZ + 10));
        }
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

    public void setTarget(GameObject target)
    {
        this.target = target;
    }
}
