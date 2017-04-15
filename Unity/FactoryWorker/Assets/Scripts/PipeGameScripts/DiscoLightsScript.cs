using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLightsScript : MonoBehaviour {

    private Light discoLight;
    private Color color;
    private Color[] discoColors = new Color[6];
    private float timer = 0f;
    private float interval;
    public float colorChangeInterval = 1f;

	// Use this for initialization
	void Start () {

        discoLight = GetComponent<Light>();
        interval = colorChangeInterval;
        colorChangeInterval *= 100;

        discoColors[0] = Color.green;
        discoColors[1] = Color.blue;
        discoColors[2] = Color.red;
        discoColors[3] = Color.yellow;
        discoColors[4] = Color.magenta;
        discoColors[5] = Color.cyan;
    }
	
	// Update is called once per frame
	void Update () {

        //Every 1 second, change light color
        timer++;

        if(timer > interval)
        {
            color = discoColors[Random.Range(0, 5)];
            interval += colorChangeInterval;
            discoLight.color = color;
        }
	}
}
