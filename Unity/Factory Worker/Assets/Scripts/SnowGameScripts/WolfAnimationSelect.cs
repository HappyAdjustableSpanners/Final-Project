using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimationSelect : MonoBehaviour {

    public enum State { Sit, Snoop, Lie };
    public State currentState;
    public bool lookAtPlayer = false;

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

        //If we want the wolf to look at the player
        if(lookAtPlayer)
        {
            StartCoroutine("DelayThenTurnOffAnimator");
        }
	}

    private IEnumerator DelayThenTurnOffAnimator()
    {
        //Wait for a second to make sure the triggered animation has finished
        yield return new WaitForSeconds(1f);

        //Disable the animator so the head can move
        anim.enabled = false;
    }
}
