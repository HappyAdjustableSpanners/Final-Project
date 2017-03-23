using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchBehaviour : MonoBehaviour {

    //The current state of the agent
    private enum FetchState { fetchingStick, deliveringStickToPlayer, waitingForPlayerToThrowStick }
    private FetchState fetchState = FetchState.fetchingStick;

    //Our stick game object and rigidbody
    private GameObject stick;
    private Rigidbody stickRb;

    //The position in the wolfs mouth that the stick will go to whilst being held
    public Transform pickUpPosition;

    //The player's position
    public Transform playerPos;

    //Get a reference to our animation controller
    private Animator anim;

    //Our Nav Mesh agent component
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private InteractableItem stickInteractableItemBehaviour;

    // Use this for initialization
    void Start () {

        //Nav Mesh Agent script
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //Wolf head position
        //headPosition = transform.Find("Armature/Root/Body/Spine4/Spine3/Spine2/Spine1/Head").transform;

        //Stick GameObject and rigidbody
        stick = GameObject.FindGameObjectWithTag("FetchStick");
        stickRb = stick.GetComponent<Rigidbody>();

        //Animator
        anim = GetComponent<Animator>();

        //Get the stick's interactable item behaviour
        //stickInteractableItemBehaviour = stick.GetComponent<InteractableItem>();

    }
	
	// Update is called once per frame
	void Update () {

        //If stick is not null and our state is fetch stick
        if (fetchState == FetchState.fetchingStick)
        {
            //Move towards stick
            FetchStick();
        }
        else if (fetchState == FetchState.deliveringStickToPlayer)
        {
            //Move towards the player
            MoveToPlayer();
        }
        else if( fetchState == FetchState.waitingForPlayerToThrowStick)
        {
        }
	}

    private void FetchStick()
    {
        navMeshAgent.Resume();
        //Go to stick position
        navMeshAgent.SetDestination(stick.transform.position);

        //have we reached the end of the path?
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 10f)
                {
                    //Change stick to not use gravity
                    stickRb.useGravity = false;

                    stickRb.velocity = Vector3.zero;
                    stickRb.angularVelocity = Vector3.zero;

                    //Parent stick to wolf
                    stick.transform.parent = transform.Find("Armature/Root/Body/Spine4/Spine3/Spine2/Spine1/Head/");

                    //Move stick to pick up position
                    stick.transform.position = pickUpPosition.position;

                    //Change state to deliver to player
                    fetchState = FetchState.deliveringStickToPlayer;
                }
            }
        }
    }

    private void MoveToPlayer()
    {

        navMeshAgent.Resume();
        //Go to stick position
        navMeshAgent.SetDestination(playerPos.position);

        //have we reached the end of the path?
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 10f)
                {
                    //Stop and sit
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") && !anim.GetCurrentAnimatorStateInfo(0).IsName("SitLook"))
                    {
                        anim.SetTrigger("Sit");
                        navMeshAgent.Stop();
                        fetchState = FetchState.waitingForPlayerToThrowStick;
                    }
                }
            }
        }
    }

    //Called when the stick hits the ground
    public void setFetch(bool value)
    {
        if (value == true)
        {
            //Set fetch state to fetching stick
            fetchState = FetchState.fetchingStick;

            //Set anim to running
            anim.SetTrigger("Running");
        }
        else
            fetchState = FetchState.deliveringStickToPlayer;
    }

    public bool isWaitingForPlayer()
    {
        if (fetchState == FetchState.waitingForPlayerToThrowStick)
        {
            return true;
        }
        else
            return false;
    }

    public void setDeliveringToPlayer(bool value)
    {
        if(value == true)
        {
            anim.SetTrigger("Running");
            fetchState = FetchState.deliveringStickToPlayer;
        }
    }
}
