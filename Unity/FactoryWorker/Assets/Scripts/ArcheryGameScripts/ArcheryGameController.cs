using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcheryGameController : MonoBehaviour {

    //Archery targets
    private GameObject[] targets;
    private int targetIndex = 0;

    //Scene management
    private ReadSceneNames readSceneNamesBehaviour;
    private string[] sceneNames;

    //Stats
    private float timer = 0f;
    private float accuracy = 0;

    //Text Meshes
    private TextMesh bestTimeText;
    private TextMesh bestAccuracyText;
    private TextMesh currentScoreText;
    private TextMesh currentAccuracyText;

    //Arrow fade scripts
    private FadeScript arrowHighScore, arrowBestAccuracy;

    //Longbow asset
    private Valve.VR.InteractionSystem.Longbow longBow;
   
    //Audio
    private AudioSource audioSource;
    private AudioClip successBeep;
    private AudioClip applause;

    //Win
    private Light discoLights;
    public GameObject balloon;
    private Valve.VR.InteractionSystem.Balloon balloonScript;
    public Transform balloonTransform;
    public bool win = false;
    

    // Use this for initialization
    void Start () {

        //Start the music player
        MusicPlayer.StartMusic(Resources.Load<AudioClip>("Audio/levelHubMusic"));

        //Set the high scores
        PlayerPrefs.SetFloat("High Score", 100f);
        PlayerPrefs.SetFloat("Best Accuracy", 0f);

        //Lights
        discoLights = GameObject.Find("Win").GetComponent<Light>();
        discoLights.enabled = false;

        //Balloon
        balloonScript = balloon.GetComponent<Valve.VR.InteractionSystem.Balloon>();
        balloonScript.enabled = false;
        balloon.SetActive(false);

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
        applause = Resources.Load<AudioClip>("Audio/applause");

        //Get arrow fade scripts
        arrowHighScore = GameObject.Find("GameLevel").transform.Find("Arrows/Arrow/arrow").GetComponent<FadeScript>();
        arrowBestAccuracy = GameObject.Find("GameLevel").transform.Find("Arrows/Arrow2/arrow").GetComponent<FadeScript>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!win)
        {
            //Increment the timer
            if (targetIndex > 0)
            {
                timer += Time.deltaTime;
            }

            //Update the current score billboard
            currentScoreText.text = Mathf.Round(timer).ToString();
        }
        else
        { 
            //If we have won
            if (balloon)
            {
                //Move balloon down from the sky to the ground
                balloon.transform.position = Vector3.Lerp(balloon.transform.position, balloonTransform.position, Time.deltaTime);
            }
        }
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

        //If we have hit the last target
        if (targetIndex == targets.Length)
        {
            if (timer < PlayerPrefs.GetFloat("High Score") || accuracy > PlayerPrefs.GetFloat("Best Accuracy"))
            {
                //Play applause sound
                audioSource.clip = applause;
                audioSource.Play();

                //Enable disco lights and disable main light
                discoLights.enabled = true;

                //Enable balloon behaviours
                balloonScript.enabled = true;
                balloon.SetActive(true);

                //Set win to true
                win = true;

                if(timer < PlayerPrefs.GetFloat("High Score"))
                {
                    arrowHighScore.FadeIn();
                }

                if (accuracy > PlayerPrefs.GetFloat("Best Accuracy"))
                {
                    arrowBestAccuracy.FadeIn();
                }
            }

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
