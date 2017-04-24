using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeGameManager : MonoBehaviour {

    //Easy build levels. Just build a level, and enter the number of pipes and platforms
    public int numPipes = 0;
    public int numPlatforms = 0;
    public int numObstacles = 0;

    //Pipe and platform game objects
    private GameObject[] platforms;
    private GameObject[] pipes;
    private GameObject[] obstacles;

    //Keep track of how many pipes and platforms are ready
    private int pipesReady, platformsReady, obstaclesReady = 0;

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
    public static int numLevels = 15;

    //High score (Player prefs)
    private int topLevel;

    //have we finished the level?
    private bool ready = false;

    //Level select board
    private GameObject[] levelSquares = new GameObject[15];

    //Level hub
    private GameObject levelHub;

    //Grab sphere
    private GameObject grabSphere;

    private int currentLevel;
    private AudioSource audioSource;
    private AudioClip failBeep, winbeep;

    void Awake()
    {
        //Load the level hub model and spawn it 
        levelHub = Resources.Load<GameObject>("Prefabs/level_hub");
        Instantiate(levelHub, new Vector3(-2.46f, -5.94f, -2.54f), levelHub.transform.Find("LevelHubPos").transform.rotation);
        
        //Start the music player
        MusicPlayer.StartMusic(Resources.Load<AudioClip>("Audio/pipeGameMusic"));

        //Get pipes and platform game objects
        platforms = GameObject.FindGameObjectsWithTag("GamePlatform");
        pipes = GameObject.FindGameObjectsWithTag("GamePipe");
        obstacles = GameObject.FindGameObjectsWithTag("GameObstacle");

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

        foreach(GameObject go in obstacles)
        {
            go.AddComponent<AudioSource>();
        }
    }
    // Use this for initialization
    void Start () {

        //Uncomment the line below to reset the top score from user prefs
        PlayerPrefs.SetInt("TopLevel", 1);

        //Load in player prefs top level
        topLevel = PlayerPrefs.GetInt("TopLevel");

        //Find pipes and platforms, and count them
        numPipes = pipes.Length;
        numPlatforms = platforms.Length;
        numObstacles = obstacles.Length;

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
        winbeep = Resources.Load<AudioClip>("Audio/win_beep2");
        audioSource.volume = 0.3f;

        //Spawn grab sphere
        Transform grabSphereSpawnPos = Resources.Load<Transform>("Prefabs/GrabSpherePosition").transform;
        GameObject grabSphere = Instantiate(Resources.Load<GameObject>("Prefabs/GrabSphereHome"), grabSphereSpawnPos.position, grabSphereSpawnPos.rotation);
        grabSphere.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        grabSphere.tag = "levelhub";


        SetUpScene(); 
              
    }

    private void SetUpScene()
    {
        //Apply floor material
        GameObject.Find("Environment").transform.Find("Floor").GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Models/Levelhub/M_Hub");

        //Get room walls
        Transform[] walls = GameObject.Find("Environment").transform.Find("room_hub").GetComponentsInChildren<Transform>();

        //Add mesh colliders for each wall
        foreach (Transform t in walls)
        {
            t.gameObject.AddComponent<MeshCollider>();
        }

        // Assign pedestal materials
        GameObject pedestal = GameObject.Find("GameLevel/Pedestal");
        Material[] tempMats = { Resources.Load<Material>("Materials/Models/LevelBoard/M_LevelBoard"), Resources.Load<Material>("Materials/PlainColors/M_White") };
        pedestal.GetComponent<Renderer>().materials = tempMats;
    }

    public void CheckFinished()
    {
        //If all pipes are ready, continue, else there must still be more pipes to hit, so we spawn another game ball
        if (pipesReady == numPipes)
        {
            //If all platforms are ready
            if (platformsReady == numPlatforms && obstaclesReady == numObstacles)
            {
                ready = true;
            }
            else
                ready = false;

            //If we have hit all pipes and platforms, continue to next level, else, reset the level
            if (ready)
            {

                //Play win audio
                audioSource.clip = winbeep;
                audioSource.Play();

                //Get next scene index
                int index = SceneManager.GetActiveScene().buildIndex + 1;

                //Check if we need to update our top level
                if (index < numLevels && index > PlayerPrefs.GetInt("TopLevel"))
                {
                    PlayerPrefs.SetInt("TopLevel", index);
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
        {
            //Play win beep sound as we have triggered a new pipe
            audioSource.clip = winbeep;
            audioSource.Play();

            //Spawn a new game ball
            SpawnGameBall();
        }
    }

    public void LoadNextScene()
    {
        //Get next scene index
        int index = SceneManager.GetActiveScene().buildIndex + 1;

        //Check if this index is not past our top level and this is not our final level
        if( index < (startLevel + topLevel) && index != finalLevel)
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
        audioSource.clip = failBeep;
        audioSource.Play();

        //Spawn a new ball
        SpawnGameBall();

        //Reset num pipes ready and num platforms ready to 0
        pipesReady = 0;
        platformsReady = 0;
        obstaclesReady = 0;

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

        foreach (GameObject go in obstacles)
        {
            //This will change the go color back to red
            go.GetComponent<ObstacleScript>().Reset();
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

    //Signal that an obstacle is ready
    public void ObstacleReady()
    {
        obstaclesReady++;
    }

    //Return the top level 
    public static int getTopLevel()
    {
        return PlayerPrefs.GetInt("TopLevel");
    }
}
