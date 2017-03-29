using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainterGameManager : MonoBehaviour {

    FadeInOutTextMesh[] textMeshBehaviours;
    FadeScript arrowFadeScript;

	// Use this for initialization
	void Start () {

        arrowFadeScript = GameObject.Find("UI/GameHints/TextMesh/arrow").GetComponent<FadeScript>();

        textMeshBehaviours = GameObject.Find("UI/GameHints").GetComponentsInChildren<FadeInOutTextMesh>();

        StartCoroutine("delayBeforeFadeInGameHints");
	}

    private IEnumerator delayBeforeFadeInGameHints()
    {
        yield return new WaitForSeconds(10f);

        foreach (FadeInOutTextMesh fadeScript in textMeshBehaviours)
        {
            fadeScript.FadeIn();
        }
        arrowFadeScript.FadeIn();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
