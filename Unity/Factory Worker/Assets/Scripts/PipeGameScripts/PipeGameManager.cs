﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PipeGameManager : MonoBehaviour {

    //Easy build levels. Just build a level, and enter the number of pipes and platforms
    public int numPipes = 3;
    public int numPlatforms = 3;

    //Pipe and platform game objects
    private GameObject[] platforms;
    private GameObject[] pipes;

    //Keep track of how many pipes and platforms are ready
    private int pipesReady, platformsReady = 0;

    //Pipe spawn script
    private PipeSpawnScript pipeSpawnScript;

    //Scene management
    private ReadSceneNames readSceneNamesBehaviour;
    private string[] sceneNames;

    //First and last level
    private int startLevel;
    private int finalLevel;

    //Level management
    private bool[] levelsComplete;
    private int numLevels = 15;

    //High score (Player prefs)
    private int topLevel;

    //have we finished the level?
    private bool ready = false;

    //Level select board
    private GameObject[] levelSquares = new GameObject[15];

    private int currentLevel;
    private AudioSource audioSource;
    private AudioClip failBeep;

    void Awake()
    {
        //Get pipes and platform game objects
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("GamePlatform");
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("GamePipe");

        //Attach audio sources to game controller and spawn pipe
        GameObject.FindGameObjectWithTag("GameController").AddComponent<AudioSource>();
        GameObject.FindGameObjectWithTag("SpawnPipe").AddComponent<AudioSource>();
        
        //Attach audio sources to pipes and platforms
        foreach (GameObject go in platforms)
        {
            go.AddComponent<AudioSource>();
        }

        foreach (GameObject go in pipes)
        {
            go.AddComponent<AudioSource>();
        }
    }
    // Use this for initialization
    void Start () {

        //Uncomment the line below to reset the top score from user prefs
        //PlayerPrefs.SetInt("TopLevel", 0);

        //Load in player prefs top level
        topLevel = PlayerPrefs.GetInt("TopLevel");

        //Find pipes and platforms, and count them
        platforms = GameObject.FindGameObjectsWithTag("GamePlatform");
        pipes = GameObject.FindGameObjectsWithTag("GamePipe");
        numPipes = pipes.Length;
        numPlatforms = platforms.Length;

        //Spawn the game ball initially
        pipeSpawnScript = GameObject.FindGameObjectWithTag("SpawnPipe").GetComponent<PipeSpawnScript>();
        pipeSpawnScript.SpawnGameObject();

        //Get scene information
        readSceneNamesBehaviour = GetComponent<ReadSceneNames>();
        sceneNames = readSceneNamesBehaviour.getNames();

        //Get level squares
        for(int i = 0; i < levelSquares.Length; i++)
        {
            //level square we are looking for
            string levelSquare = "Square" + (i + 1);

            //find level squares on level select board gameobject
            levelSquares[i] = GameObject.Find("GameLevel").transform.Find("next_level_sign").Find(levelSquare).gameObject;
 
        }

        //Get the start and final level
        startLevel = 1;
        finalLevel = levelSquares.Length + 1;

        //Color all the levels we have done, green
        for (int i = 0; i < topLevel; i++)
        {
            levelSquares[i].GetComponent<Renderer>().material.color = Color.green;
        }
        //Color all the levels we still have to do red
        for (int i = topLevel; i < numLevels; i++)
        {
            levelSquares[i].GetComponent<Renderer>().material.color = Color.red;
        }

   
        //Get the current level
        currentLevel = SceneManager.GetActiveScene().buildIndex - startLevel;

        //Color the current level square yellow so we know what level we are on
        levelSquares[currentLevel].GetComponent<Renderer>().material.color = Color.yellow;

        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Get audio clip from resources and load it
        failBeep = Resources.Load<AudioClip>("Audio/fail_beep");
        audioSource.clip = failBeep;
    }

    public void CheckFinished()
    {
        //If all pipes are ready, continue, else there must still be more pipes to hit, so we spawn another game ball
        if (pipesReady == numPipes)
        {
            //If all platforms are ready
            if (platformsReady == numPlatforms)
            {
                ready = true;
            }
            else
                ready = false;

            //If we have hit all pipes and platforms, continue to next level, else, reset the level
            if (ready)
            {
                //Get next scene index
                int index = SceneManager.GetActiveScene().buildIndex + 1;

                //Check if we need to update our top level
                if (index - startLevel > PlayerPrefs.GetInt("TopLevel"))
                {
                    PlayerPrefs.SetInt("TopLevel", index - startLevel);
                }

                //If we are not at the final level, Load the scene using SteamVR_LoadLevel
                if (index != finalLevel)
                {
                    SteamVR_LoadLevel.Begin(sceneNames[index], false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
                }
                else
                {
                    //If we have completed all levels, go back to the hub
                    SteamVR_LoadLevel.Begin("PipeGameWin", false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
                }
            }
            else
                Reset();
        }
        else
            SpawnGameBall();
    }

    public void LoadNextScene()
    {
        //Get next scene index
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        //Check if this index is not past our top level and this is not our final level
        if( index < (startLevel + topLevel + 1) && index != finalLevel)
        {
            SteamVR_LoadLevel.Begin(sceneNames[index], false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void LoadPrevScene()
    {
        //Load the scene with the previous build index
  
        //Get prev scene index
        int index = SceneManager.GetActiveScene().buildIndex - 1;

        //Make sure we are in bounds
        if (index >= startLevel)
        {
            SteamVR_LoadLevel.Begin(sceneNames[index], false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void Reset()
    {
        //Play audio
        audioSource.Play();

        //Spawn a new ball
        SpawnGameBall();

        //Reset num pipes ready and num platforms ready to 0
        pipesReady = 0;
        platformsReady = 0;

        //Reset all platforms and pipes
        foreach (GameObject go in platforms)
        {
            //This will change the go color back to red
            go.GetComponent<PlatformScript>().Reset();
        }

        foreach (GameObject go in pipes)
        {
            //This will change the go color back to red
            go.GetComponent<PipeScript>().Reset();
        }
    }

    //Spawn the game ball
    public void SpawnGameBall()
    {
        pipeSpawnScript.SpawnGameObject();
    }

    //Signal that a pipe is ready
    public void PipeReady()
    {
        pipesReady++;
    }

    //Signal that a platform is ready
    public void PlatformReady()
    {
        platformsReady++;
    }
}
