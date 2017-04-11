using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationScript : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        //Get the animator and trigger the float animation
        anim = GetComponent<Animator>();
        anim.SetTrigger("Float");
	}
}
