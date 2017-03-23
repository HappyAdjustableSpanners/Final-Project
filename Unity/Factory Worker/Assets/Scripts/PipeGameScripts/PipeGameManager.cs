using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PipeGameManager : MonoBehaviour {

    public int numPipes = 3;
    public int numPlatforms = 3;

    private int pipesReady, platformsReady = 0;

    private GameObject[] platforms;
    private GameObject[] pipes;
    private PipeSpawnScript pipeSpawnScript;
    private ReadSceneNames readSceneNamesBehaviour;
    private string[] sceneNames;
    private bool[] levelsComplete;
    private int numLevels = 15;
    private int topLevel;
    private int startSceneIndex;
    private bool ready = false;
    public GameObject[] levelSquares;
    public int finalLevel;
    private int currentLevel;
    private GameObject currentLevelCircle, newCurrentLevelCircle;
    private AudioSource audioSource;
    private AudioClip failBeep;

    void Awake()
    {
        //Attach audio sources to relavent objects
        GameObject.FindGameObjectWithTag("GameController").AddComponent<AudioSource>();
        GameObject.FindGameObjectWithTag("SpawnPipe").AddComponent<AudioSource>();
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("GamePlatform");
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("GamePipe");

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

        //Get the index of the first pipe scene
        startSceneIndex = 1;

        //Get level squares
        for(int i = 0; i < levelSquares.Length; i++)
        {
            //level square we are looking for
            string levelSquare = "Square" + (i + 1);

            //find level squares on level select board gameobject
            levelSquares[i] = GameObject.Find("GameLevel").transform.Find("next_level_sign").Find(levelSquare).gameObject;
 
        }

        //Color all the levels we have done, green
        for(int i = 0; i < topLevel; i++)
        {
            levelSquares[i].GetComponent<Renderer>().material.color = Color.green;
        }
        //Color all the levels we still have to do red
        for (int i = topLevel; i < numLevels; i++)
        {
            levelSquares[i].GetComponent<Renderer>().material.color = Color.red;
        }

        //Get the final level
        finalLevel = levelSquares.Length + 1;
   
        //Get the current level
        currentLevel = SceneManager.GetActiveScene().buildIndex - startSceneIndex;

        //Load level circle from resources
        //currentLevelCircle = Resources.Load<GameObject>("Prefabs/CurrentLevelCircle");
        //newCurrentLevelCircle = Instantiate(currentLevelCircle, levelSquares[currentLevel].transform.position, currentLevelCircle.transform.rotation);

        //Move back to align with level square
        //newCurrentLevelCircle.transform.position = new Vector3(currentLevelCircle.transform.position.x, currentLevelCircle.transform.position.y, currentLevelCircle.transform.position.z + 0.23f);

        //
        levelSquares[currentLevel].GetComponent<Renderer>().material.color = Color.yellow;

        //Get audio source
        audioSource = GetComponent<AudioSource>();

        //Get audio clip from resources and load it
        failBeep = Resources.Load<AudioClip>("Audio/fail_beep");
        audioSource.clip = failBeep;
    }

    public void CheckFinished()
    {
        //If all pipes are ready
        if (pipesReady == numPipes)
        {
            //If all platforms are ready
            if (platformsReady == numPlatforms)
            {
                ready = true;
            }
            else
                ready = false;


            if (ready)
            {
                //Get next scene index
                int index = SceneManager.GetActiveScene().buildIndex + 1;

                //Check if we need to update our top level
                if (index - startSceneIndex > PlayerPrefs.GetInt("TopLevel"))
                {
                    PlayerPrefs.SetInt("TopLevel", index - startSceneIndex);
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
        //If the controller
        

        //Get next scene index
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        //Check if this index is not past our top level and this is not our final level
        if( index < (startSceneIndex + topLevel + 1) && index != finalLevel)
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
        if (index >= startSceneIndex)
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

    public void SpawnGameBall()
    {
        pipeSpawnScript.SpawnGameObject();
    }

    public void PipeReady()
    {
        pipesReady++;
    }

    public void PlatformReady()
    {
        platformsReady++;
    }
}
