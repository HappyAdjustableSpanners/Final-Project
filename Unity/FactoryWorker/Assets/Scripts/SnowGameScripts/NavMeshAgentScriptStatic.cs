using UnityEngine;
using System.Collections;

public class NavMeshAgentScriptStatic : MonoBehaviour {

    public bool sitLook, lie;
    private Animator anim;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

        if (sitLook)
        {
            anim.SetTrigger("Sit");
        }
        else if( lie )
        {
            anim.SetTrigger("Lie");
        }
	}

	
}
