using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGameController : MonoBehaviour {

    private FadeInOutTextMesh[] textFadeScripts;
    private FadeScript arrowFadeScript;

	// Use this for initialization
	void Awake () {

        //Start the music player
        MusicPlayer.StartMusic(Resources.Load<AudioClip>("Audio/snowGameMusic"));

        //Get the text mesh fade scripts and the UI arrow fade script
        arrowFadeScript = GameObject.Find("UI/GameHints/TextMesh/arrow").GetComponent<FadeScript>();
        textFadeScripts = GameObject.Find("UI/GameHints").GetComponentsInChildren<FadeInOutTextMesh>();

        //Fade in the game hints after the delay
        StartCoroutine("delayBeforeFadeInGameHints");

	}

    private IEnumerator delayBeforeFadeInGameHints()
    {
        yield return new WaitForSeconds(10f);

        //After 10 second delay, fade in the game hints UI elements
        foreach (FadeInOutTextMesh f in textFadeScripts)
        {
            f.FadeIn();
        }
        arrowFadeScript.FadeIn();
    }
}