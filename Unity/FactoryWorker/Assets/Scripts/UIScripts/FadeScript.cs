using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour {

    //Renderer
    private Renderer rend;

    //Whether we are fading in or out
    private bool fadeIn;

    private Color fadeOutColor, fadeInColor;

	// Use this for initialization
	void Start () {

        rend = GetComponentInChildren<Renderer>();

        fadeOutColor = new Color(rend.material.GetColor("_TintColor").r, rend.material.GetColor("_TintColor").g, rend.material.GetColor("_TintColor").b, -1f);
        fadeInColor = new Color(rend.material.GetColor("_TintColor").r, rend.material.GetColor("_TintColor").g, rend.material.GetColor("_TintColor").b, 1f);
    }
	
	// Update is called once per frame
	void Update () {

        Color newColor = Color.black;

        if(fadeIn)
        {
            newColor = Color.Lerp(rend.material.GetColor("_TintColor"), fadeInColor, Time.deltaTime);

        }
        if(!fadeIn)
        {
            newColor = Color.Lerp(rend.material.GetColor("_TintColor"), fadeOutColor, Time.deltaTime);
        }

        rend.material.SetColor("_TintColor", newColor);
    }

    public void FadeOut()
    {
        fadeIn = false;
    }

    public void FadeIn()
    {
        fadeIn = true;
    }
}
