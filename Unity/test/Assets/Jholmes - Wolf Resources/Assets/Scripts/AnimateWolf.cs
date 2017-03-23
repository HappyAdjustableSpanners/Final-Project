using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWolf : MonoBehaviour {

    //Animator component
    private Animator anim;

    //List of possible states
    public enum State { Sit, LieDown, Walk, Run, Track, Howl, Curious };

    //Default State will be walk
    public State state = State.Walk;

	// Use this for initialization
	void Start () {

        //Get our animator component
        anim = GetComponent<Animator>();

        //Set trigger for appropriate animation
        switch(state)
        {
            case State.Sit:
                anim.SetTrigger("Sit");
                break;
            case State.LieDown:
                anim.SetTrigger("LieDown");
                break;
            case State.Walk:
                anim.SetTrigger("Walk");
                break;
            case State.Run:
                anim.SetTrigger("Run");
                break;
            case State.Track:
                anim.SetTrigger("Track");
                break;
            case State.Howl:
                anim.SetTrigger("Howl");
                break;
            case State.Curious:
                anim.SetTrigger("Curious");
                break;
        }
	}
}
