using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutTextMesh : MonoBehaviour {

    //Text mesh
    private TextMesh[] textMeshes;

    //Line Renderer
    private LineRenderer lineRenderer;

    //Fading
    public float fadeSpeed = 2f;

    //fade in or out
    private bool fadeIn = true;

	// Use this for initialization
	void Start () {

        //Get text mesh component
        textMeshes = GetComponentsInChildren<TextMesh>();

        foreach (TextMesh textMesh in textMeshes)
        {
            //Set to 0 alpha so it is invisible
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);
        }

        //Get line renderer
        lineRenderer = GetComponent<LineRenderer>();
	}

    public void FadeIn()
    {
        fadeIn = true;
    }

    public void FadeOut()
    {
        fadeIn = false;
    }

    void Update()
    {
        if(fadeIn)
        {
            foreach (TextMesh textMesh in textMeshes)
            {
                //Fade in hint
                textMesh.color = Color.Lerp(textMesh.color, new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f), Time.deltaTime * fadeSpeed);
                lineRenderer.startColor = Color.Lerp(textMesh.color, new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, 1f), Time.deltaTime * fadeSpeed);
                lineRenderer.endColor = Color.Lerp(textMesh.color, new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, 1f), Time.deltaTime * fadeSpeed);
            }
        }
        else
        {
            foreach (TextMesh textMesh in textMeshes)
            {
                //Fade out
                textMesh.color = Color.Lerp(textMesh.color, new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, -1f), Time.deltaTime * fadeSpeed);
                lineRenderer.startColor = Color.Lerp(textMesh.color, new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, -1f), Time.deltaTime * fadeSpeed);
                lineRenderer.endColor = Color.Lerp(textMesh.color, new Color(lineRenderer.startColor.r, lineRenderer.startColor.g, lineRenderer.startColor.b, -1f), Time.deltaTime * fadeSpeed);

                //When the alpha has almost reached 0, set it to 0
                if (textMesh.color.a < 0.005f)
                {
                    //Set to 0 alpha so it is invisible
                    textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);
                }
            }
        }
    }
}
