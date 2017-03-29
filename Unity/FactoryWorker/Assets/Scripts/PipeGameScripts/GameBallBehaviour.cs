using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBallBehaviour : MonoBehaviour {

    private bool dissolve;
    private Renderer rend;
    private SphereCollider sphereCollider;
    private AudioSource audioSource;
    private AudioClip bounce1, bounce2, bounce3;

    public float dissolveSpeed = 2f;

	// Use this for initialization
	void Start () {
        //Renderer
        rend = GetComponent<Renderer>();

        //Collider
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Awake()
    {
        //Audio
        audioSource = GetComponent<AudioSource>();

        //Clips
        bounce1 = Resources.Load<AudioClip>("Audio/bounce1");
        bounce2 = Resources.Load<AudioClip>("Audio/bounce2");
        bounce3 = Resources.Load<AudioClip>("Audio/bounce3");
    }
	
	// Update is called once per frame
	void Update () {

        //If dissolve, increase the slice until the ball is not visible
		if(dissolve)
        {
            //Increase the slice amount
            rend.material.SetFloat("_SliceAmount", Mathf.Lerp(rend.material.GetFloat("_SliceAmount"), 1, Time.deltaTime * dissolveSpeed));

            sphereCollider.enabled = false;

            //If the object is almost completely sliced, destroy it
            if(rend.material.GetFloat("_SliceAmount") > 0.99f)
            {
                Destroy(gameObject);
            }
        }
	}
    
    public void setDissolve( bool value )
    {
        dissolve = value;
    }

    private void OnCollisionEnter(Collision col)
    {
        //Choose a random number between 1 and 3 (max is exclusive when using ints)
        int randomNum = Random.Range(1, 4);

        if(randomNum == 1)
        {
            audioSource.clip = bounce1;
        }
        else if(randomNum == 2)
        {
            audioSource.clip = bounce2;
        }
        else if (randomNum == 3)
        {
            audioSource.clip = bounce3;
        }

        //Play bounce sound
        audioSource.Play();
    }
}
