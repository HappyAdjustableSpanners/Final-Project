using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimationSelect : MonoBehaviour {

    public enum State { Sit, Snoop, Lie };
    public State currentState;
    private Animator anim;

	// Use this for initialization
	void Start () {
        //Get animator
        anim = GetComponent<Animator>();

        //Set the relevant trigger on the animator
        switch (currentState)
        {
            case State.Sit:
                anim.SetTrigger("Sit");
                break;
            case State.Lie:
                anim.SetTrigger("Lie");
                break;
            case State.Snoop:
                anim.SetTrigger("Snoop");
                break;
        }
	}
}
