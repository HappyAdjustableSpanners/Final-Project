using UnityEngine;
using System.Collections;

public class AnimatorScriptStatic : MonoBehaviour {

	private Animator anim;

	public bool sitting;
	public bool lyingDown;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		if (sitting) {
			anim.SetTrigger("Sit");
		}
		else if (lyingDown)
		{
			anim.SetTrigger ("Lie");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
