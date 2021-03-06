﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryGameManager : MonoBehaviour {

    //Public varaibles
    public float startTimer = 30f;
    public float intervalDecrement = 2f;

    //Timers
    private float timer;
    private float currentInterval;

    //Stats
    private int targetsHit = 0;

    //Stats board
    TextMesh timeRemainingText, currentScoreText, highScoreText, currentIntervalText, balloonText, balloonTextBacking;

    //High scores
    private int targetsHitHighScore;

    //Win conditions
    private bool haveFinished = false;

    //Balloon
    public GameObject balloon;
    public Transform balloonDestination;
    private Valve.VR.InteractionSystem.Balloon balloonScript;

    //Audio
    private AudioSource audioSource, tickAudioSource;
    private AudioClip successBeep, applause, failBeep;

    //Archery targets
    private GameObject[] targets;
    private int targetIndex = 0;

    //Started game
    private bool gameRunning = false;

    //Mascot
    private GameObject mascot;
    private Vector3 mascotSpawnPosition;

    //Interactables
    private GameObject playAgainGrabSphere;

    // Use this for initialization
    void Start () {

        //Set the highscore manually (for debug)
        //PlayerPrefs.SetInt("targetsHitHighScore", 0);

        //Get interactables
        playAgainGrabSphere = GameObject.Find("Interactables").transform.Find("PlayAgainGrabSphere").gameObject;
        playAgainGrabSphere.SetActive(false);

        //Get the highscores
        targetsHitHighScore = PlayerPrefs.GetInt("targetsHitHighScore");

        //Start the music player
        MusicPlayer.StartMusic(Resources.Load<AudioClip>("Audio/levelHubMusic"));

        //init timer value and currentInterval to startTimer
        timer = startTimer;
        currentInterval = startTimer;

        //Initialise text elements
        timeRemainingText = GameObject.Find("GameLevel/StatsBoard/TimeRemainingValue").GetComponent<TextMesh>();
        currentIntervalText = GameObject.Find("GameLevel/StatsBoard/CurrentIntervalValue").GetComponent<TextMesh>();
        currentScoreText = GameObject.Find("GameLevel/ScoreBoard/CurrentScoreValue").GetComponent<TextMesh>();
        highScoreText = GameObject.Find("GameLevel/ScoreBoard/HighScoreValue").GetComponent<TextMesh>();
        balloonText = GameObject.Find("Win/Balloon/instructions_board/Text/YouveBeatenHighScoreText").GetComponent<TextMesh>();
        balloonTextBacking = GameObject.Find("Win/Balloon/instructions_board/Text/YouveBeatenHighScoreTextBacking").GetComponent<TextMesh>();


        //Set text values
        highScoreText.text = PlayerPrefs.GetInt("targetsHitHighScore").ToString();
        currentIntervalText.text = currentInterval.ToString();

        //Balloon script
        balloonScript = balloon.GetComponent<Valve.VR.InteractionSystem.Balloon>();
        balloonScript.enabled = false;
        balloon.SetActive(false);

        //Audio
        audioSource = GetComponent<AudioSource>();
        tickAudioSource = GameObject.Find("TickAudio").GetComponent<AudioSource>();
        successBeep = Resources.Load<AudioClip>("Audio/win_beep");
        applause = Resources.Load<AudioClip>("Audio/applause");
        failBeep = Resources.Load<AudioClip>("Audio/fail_beep");

        //Targets
        targets = GameObject.FindGameObjectsWithTag("ArcheryTarget");

        //Disable all targets
        foreach (GameObject target in targets)
        {
            target.SetActive(false);
        }

        //Activate the first target
        targets[0].SetActive(true);

        //Get mascot
        mascot = Resources.Load<GameObject>("Prefabs/Mascot");
        mascotSpawnPosition = GameObject.Find("GameLevel").transform.Find("MascotSpawnPosition").transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        if (gameRunning)
        {
            //Start the ticking sound
            if(!tickAudioSource.enabled)
            {
                tickAudioSource.enabled = true;
                tickAudioSource.Play();
            }

            //If timer reaches zero, game over
            if (timer > 0)
            {
                //Decrement timer and round it
                timer -= Time.deltaTime;
            }
            else
            {
                //Enable balloon behaviour
                balloonScript.enabled = true;
                balloon.SetActive(true);

                //Set win to true
                haveFinished = true;

                //Check if high score has been beaten
                if (targetsHit > targetsHitHighScore)
                {
                    //High score has been beaten
                    PlayerPrefs.SetInt("targetsHitHighScore", targetsHit);

                    //Play applause sound
                    audioSource.clip = applause;
                    audioSource.Play();
                }
                else
                {
                    //If we did not beat the high score
                    //Play sad audio
                    audioSource.clip = failBeep;
                    audioSource.Play();

                    //Change the balloon text
                    balloonText.text = "Better luck \n next time!";
                    balloonTextBacking.text = "Better luck \n next time!";
                }

                //Disable all targets
                foreach (GameObject target in targets)
                {
                    target.SetActive(false);
                }

                //Spawn the mascot
                Instantiate(mascot, mascotSpawnPosition, Quaternion.identity);

                //Spawn play again sphere
                playAgainGrabSphere.SetActive(true);

                //Disable the tick audio
                tickAudioSource.enabled = false;

                //Set game running to false
                gameRunning = false;
            }

            //Update the text elements
            timeRemainingText.text = timer.ToString("f1");
            currentScoreText.text = targetsHit.ToString();
        }

        if (haveFinished)
        {
            //If we have won
            if (balloon)
            {
                //Move balloon down from the sky to the ground
                balloon.transform.position = Vector3.Lerp(balloon.transform.position, balloonDestination.position, Time.deltaTime);
            }
        }
	}

    public void TargetHit()
    {

        //Start game if it is not started
        if (!gameRunning)
            gameRunning = true;

        //Play success audio
        audioSource.clip = successBeep;
        audioSource.Play();

        //If we hit a target, decrease the current interval, reset the timer to the interval
        currentInterval -= intervalDecrement;
        timer = currentInterval;

        //Update current interval text
        currentIntervalText.text = currentInterval.ToString();

        //Remove the current target
        targets[targetIndex].SetActive(false);

        //Increment targets hit
        targetsHit++;

        //Increment the target index
        targetIndex++;

        //If we have hit all targets, loop back around to first target
        if (targetIndex >= targets.Length)
        {
            targetIndex = 0;
        }

        targets[targetIndex].SetActive(true);
    }
}
