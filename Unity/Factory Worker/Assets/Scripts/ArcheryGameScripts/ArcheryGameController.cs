﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcheryGameController : MonoBehaviour {

    private GameObject[] targets;
    private int targetIndex = 0;

    //Scene management
    private ReadSceneNames readSceneNamesBehaviour;
    private string[] sceneNames;
    private float timer = 0f;
    private TextMesh bestTimeText;
    private TextMesh currentScoreText;
    private TextMesh bestAccuracyText;
    private TextMesh currentAccuracyText;
    private Valve.VR.InteractionSystem.Longbow longBow;
    private float accuracy = 0;
    private AudioSource audioSource;
    private AudioClip successBeep;

    // Use this for initialization
    void Start () {

        //Get reference to TextMesh components
        currentScoreText = GameObject.FindGameObjectWithTag("ArcheryCurrentTimeText").GetComponent<TextMesh>();
        bestTimeText = GameObject.FindGameObjectWithTag("ArcheryBestTimeText").GetComponent<TextMesh>();
        currentAccuracyText = GameObject.FindGameObjectWithTag("ArcheryCurrentAccuracyText").GetComponent<TextMesh>();
        bestAccuracyText = GameObject.FindGameObjectWithTag("ArcheryBestAccuracyText").GetComponent<TextMesh>();

        //Load the high scores
        bestTimeText.text = PlayerPrefs.GetFloat("High Score").ToString() + " Seconds";
        bestAccuracyText.text = PlayerPrefs.GetFloat("Best Accuracy").ToString();

        //Scene management
        readSceneNamesBehaviour = GetComponent<ReadSceneNames>();
        sceneNames = readSceneNamesBehaviour.getNames();

        //Get all targets
        targets = GameObject.FindGameObjectsWithTag("ArcheryTarget");

        //Disable all targets
        foreach (GameObject target in targets)
        {
            target.SetActive(false);
        }

        //Activate the first target
        targets[0].SetActive(true);

        //Get audio components
        audioSource = GetComponent<AudioSource>();
        successBeep = Resources.Load<AudioClip>("Audio/win_beep");
	}
	
	// Update is called once per frame
	void Update () {

        //Increment the timer
        if (targetIndex > 0)
        {
            timer += Time.deltaTime;
        }

        //Update the current score billboard
        currentScoreText.text = Mathf.Round(timer).ToString();
      
    }

    public void NextTarget()
    {

        //Play success audio
        audioSource.clip = successBeep;
        audioSource.Play();

        //Remove the current target
        targets[targetIndex].SetActive(false);

        //Increment the target index
        targetIndex++;

        //Update accuracy
        UpdateAccuracy();

        //Enable the next target
        if (targetIndex < targets.Length)
        {
            targets[targetIndex].SetActive(true);
        }

        if (targetIndex == targets.Length)
        {
            //Update best time
            if (timer < PlayerPrefs.GetFloat("High Score"))
            {
                //Set the new high score
                PlayerPrefs.SetFloat("High Score", Mathf.Round(timer));
            }

            //Update best accuracy
            if( accuracy > PlayerPrefs.GetFloat("Best Accuracy"))
            {
                //Set the new accuracy high score
                PlayerPrefs.SetFloat("Best Accuracy", accuracy);
            }

            //We have hit all targets
            SteamVR_LoadLevel.Begin("levelhub", false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    private void UpdateAccuracy()
    {
        //Get reference to the two controllers
        GameObject hand1 = GameObject.Find("Player/SteamVRObjects/Hand1/");
        GameObject hand2 = GameObject.Find("Player/SteamVRObjects/Hand2/");

        //Long way round of geetting reference to the bow... not ideal
        if (hand1.transform.Find("Longbow(Clone)") == null)
        {
            longBow = hand2.transform.Find("Longbow(Clone)").GetComponent<Valve.VR.InteractionSystem.Longbow>();
        }
        else
            longBow = hand1.transform.Find("Longbow(Clone)").GetComponent<Valve.VR.InteractionSystem.Longbow>();

        //If longbow is not null
        if (longBow != null)
        {
            //Get the accuracy
            accuracy = targetIndex / longBow.getArrowsFired();

            //Round to 2 dp
            accuracy = Mathf.Round(accuracy * 100) / 100;

            //Set accuracy text
            currentAccuracyText.text = accuracy.ToString();
        }
    }
}
