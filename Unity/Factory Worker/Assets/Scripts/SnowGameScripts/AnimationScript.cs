using UnityEngine;
using System.Collections;

public class AnimationScript : MonoBehaviour {

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		anim.speed = Random.Range (0.5f, 1.5f);
	}
}
