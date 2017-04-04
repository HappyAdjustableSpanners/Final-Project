using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimationSelect : MonoBehaviour {

    //State machine
    public enum State { Sit, Snoop, Lie };
    public State currentState;

    //Options
    public bool lookAtPlayer = false;
    public bool intermitentHowling = false;

    //Animator
    private Animator anim;

    //Howling
    private float howlTimer = 1f;
    private float currentHowlDelay;
    private float howlNum;
    public float averageHowlDelay = 20f;
    public float howlDelayRange = 10f;

    //Audio
    private AudioClip howl1, howl2, howl3, howl4;
    private AudioSource audioSource;


    // Use this for initialization
    void Start () {

        //Get audioSource
        audioSource = GetComponent<AudioSource>();

        //Load howl clips
        howl1 = Resources.Load<AudioClip>("Audio/Howl1");
        howl2 = Resources.Load<AudioClip>("Audio/Howl2");
        howl3 = Resources.Load<AudioClip>("Audio/Howl3");
        howl4 = Resources.Load<AudioClip>("Audio/Howl4");

        //Get animator
        anim = GetComponent<Animator>();

        //Set the relevant trigger on the animator
        TriggerAnimation(currentState);

        //If we want the wolf to look at the player
        if(lookAtPlayer)
        {
            StartCoroutine("DelayThenTurnOffAnimator");
        }

        //Get an initial howl delay
        currentHowlDelay = Random.Range(averageHowlDelay - howlDelayRange, averageHowlDelay + howlDelayRange);
    }

    private void TriggerAnimation(State state)
    {
        //Set the relevant trigger on the animator
        switch (state)
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

    void Update()
    {
        //if Intermitent howling is checked, then howl every now and again
        if (intermitentHowling)
        {
            //Increment howl timer
            howlTimer += Time.deltaTime;

            if(howlTimer > currentHowlDelay )
            {
                //Record current animation state
                State tempState = currentState;

                //Set howl trigger
                anim.SetTrigger("Howl");

                //Play random howl audio
                PlayRandomHowl();              

                //After 3 seconds, go back to original state
                StartCoroutine("RevertAnimationAfterDelay", tempState);

                //Get a new random howl delay
                currentHowlDelay = Random.Range(averageHowlDelay - howlDelayRange, averageHowlDelay + howlDelayRange);

                //Reset howl timer to 0
                howlTimer = 0;
            }
        }
    }

    private void PlayRandomHowl()
    {
        //Choose a random howl audio to play
        howlNum = Random.Range(1, 5);

        if (howlNum == 1)
        {
            audioSource.clip = howl1;
        }
        else if (howlNum == 2)
        {
            audioSource.clip = howl2;
        }
        else if (howlNum == 3)
        {
            audioSource.clip = howl3;
        }
        else if (howlNum == 4)
        {
            audioSource.clip = howl4;
        }

        audioSource.Play();
    }

    private IEnumerator RevertAnimationAfterDelay(State tempState)
    {
        yield return new WaitForSeconds(5);

        TriggerAnimation(tempState);
    }

    private IEnumerator DelayThenTurnOffAnimator()
    {
        //Wait for a second to make sure the triggered animation has finished
        yield return new WaitForSeconds(1f);

        //Disable the animator so the head can move
        anim.enabled = false;
    }
}
