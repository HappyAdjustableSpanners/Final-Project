using UnityEngine;
using System.Collections;

public class NavMeshAgentScriptMan : MonoBehaviour {

	//Our Nav Mesh agent component
	private UnityEngine.AI.NavMeshAgent navMeshAgent;

	public bool running;
	public bool walking;

	private bool stage1Complete = false;

	//Our target position
	private Transform[] targets;

	//Get a reference to our animation controller
	private Animator anim;

	// Use this for initialization
	void Start () {
		navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

		//Get our list of target positions. We will be navigating to each at one point
		targets = GameObject.FindGameObjectWithTag ("Destinations").GetComponentsInChildren<Transform> ();

		anim = GetComponent<Animator> ();

		if (running) {
			anim.SetTrigger ("Running");
		}
		else if (walking) {
			anim.SetTrigger ("Walking");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!stage1Complete) {
			//Go to first nav point
			navMeshAgent.SetDestination (targets [2].position);

			//have we reached the end of the path?
			if (!navMeshAgent.pathPending) {
				if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) {
					if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 30f) {
						if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Sit")) {

							//Look at the wolf
							transform.LookAt (GameObject.FindGameObjectWithTag ("main_wolf").transform);
							anim.SetTrigger ("Sit");
							stage1Complete = true;
						}

					}
				}
			}
		}
	}
}
