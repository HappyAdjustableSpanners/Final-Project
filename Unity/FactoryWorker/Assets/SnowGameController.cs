using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowGameController : MonoBehaviour {

    private FadeInOutTextMesh[] textFadeScripts;
    private FadeScript arrowFadeScript;

	// Use this for initialization
	void Awake () {
        arrowFadeScript = GameObject.Find("UI/GameHints/TextMesh/arrow").GetComponent<FadeScript>();
        textFadeScripts = GameObject.Find("UI/GameHints").GetComponentsInChildren<FadeInOutTextMesh>();

        StartCoroutine("delayBeforeFadeInGameHints");
	}

    private IEnumerator delayBeforeFadeInGameHints()
    {
        yield return new WaitForSeconds(10f);

        foreach (FadeInOutTextMesh f in textFadeScripts)
        {
            f.FadeIn();
        }
        arrowFadeScript.FadeIn();
    }

    // Update is called once per frame
    void Update () {
		
	}
}