using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHubGameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MusicPlayer.StartMusic(Resources.Load<AudioClip>("Audio/levelHubMusic"));
	}
}
