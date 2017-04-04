using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterGameManager : MonoBehaviour {

    FadeInOutTextMesh[] textMeshBehaviours;
    FadeScript arrowFadeScript;

	// Use this for initialization
	void Start () {

        //Start the music player
        MusicPlayer.StartMusic(Resources.Load<AudioClip>("Audio/painterGameMusic"));

        //Get the arrow fade script
        arrowFadeScript = GameObject.Find("UI/GameHints/TextMesh/arrow").GetComponent<FadeScript>();

        //Get the text mesh fade scripts of the UI game objects
        textMeshBehaviours = GameObject.Find("UI/GameHints").GetComponentsInChildren<FadeInOutTextMesh>();

        //After a delay, fade in the game hints
        StartCoroutine("delayBeforeFadeInGameHints");
	}

    private IEnumerator delayBeforeFadeInGameHints()
    {
        //After 10 second delay
        yield return new WaitForSeconds(10f);

        //Fade in the game hints and UI arrow via their fade script
        foreach (FadeInOutTextMesh fadeScript in textMeshBehaviours)
        {
            fadeScript.FadeIn();
        }
        arrowFadeScript.FadeIn();
    }
}
