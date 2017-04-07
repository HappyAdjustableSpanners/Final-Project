using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchBehaviour : MonoBehaviour {

    //The current state of the agent
    public enum FetchState { fetchingStick, deliveringStickToPlayer, waitingForPlayerToPickUpStick, waitingForPlayerToThrowStick }
    private FetchState fetchState = FetchState.fetchingStick;

    //Our stick game object and rigidbody
    private GameObject stick;
    private Rigidbody stickRb;

    //The position in the wolfs mouth that the stick will go to whilst being held
    private Transform targetStickPos;

    //The player's position
    public Transform playerPos;
    private GameObject playerHeadCam;

    //Get a reference to our animation controller
    private Animator anim;

    //Our Nav Mesh agent component
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    //Look at player script
    private LookAtPlayer lookAtPlayer;

    // Use this for initialization
    void Start () {

        //Nav Mesh Agent script
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //Stick GameObject and rigidbody
        stick = GameObject.FindGameObjectWithTag("FetchStick");
        stickRb = stick.GetComponent<Rigidbody>();
        targetStickPos = transform.Find("Armature/Root/Body/Spine4/Spine3/Spine2/Spine1/Head/StickTargetPos");

        //Animator
        anim = GetComponent<Animator>();

        //Look at player script
        lookAtPlayer = GetComponent<LookAtPlayer>();

        //Player head cam
        playerHeadCam = GameObject.Find("Player").transform.Find("SteamVRObjects/VRCamera/FollowHead").gameObject;

        //Player position
        playerPos = playerHeadCam.transform;
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
        else if (fetchState == FetchState.waitingForPlayerToPickUpStick)
        {
            lookAtPlayer.setTarget(playerHeadCam);
        }
        else if (fetchState == FetchState.waitingForPlayerToThrowStick)
        {
            lookAtPlayer.setTarget(stick);
        }
    }

    private void FetchStick()
    {
        //Resume the nav mesh agent path find
        navMeshAgent.Resume();

        //Go to stick position
        navMeshAgent.SetDestination(stick.transform.position);

        //have we reached the end of the path?
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 15f)
                {
                    //Change stick to not use gravity
                    stickRb.useGravity = false;

                    stickRb.velocity = Vector3.zero;
                    stickRb.angularVelocity = Vector3.zero;

                    //Parent stick to wolf
                    stick.transform.parent = transform.Find("Armature/Root/Body/Spine4/Spine3/Spine2/Spine1/Head/");

                    //Move stick to pick up position
                    stick.transform.position = targetStickPos.position;
                    stick.transform.rotation = targetStickPos.rotation;

                    //Change state to deliver to player
                    fetchState = FetchState.deliveringStickToPlayer;
                }
            }
        }
    }

    private void MoveToPlayer()
    {
        //Resume the nav mesh agent path finding
        navMeshAgent.Resume();

        //Go to stick position
        navMeshAgent.SetDestination(playerPos.position);

        //have we reached the end of the path?
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 20f)
                {
                    //Stop and sit
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") && !anim.GetCurrentAnimatorStateInfo(0).IsName("SitLook"))
                    {
                        anim.SetTrigger("Sit");
                        navMeshAgent.Stop();
                        fetchState = FetchState.waitingForPlayerToPickUpStick;

                        StartCoroutine("DisableAnimatorAfterDelay");
                    }
                }
            }
        }
    }

    private IEnumerator DisableAnimatorAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        anim.enabled = false;
    }

    //Gets and sets
    public void setFetch(bool value)
    {
        if (value == true)
        {
            //Set fetch state to fetching stick
            fetchState = FetchState.fetchingStick;

            anim.enabled = true;
            //Set anim to running      
            anim.SetTrigger("Running");
        }
        else
            fetchState = FetchState.deliveringStickToPlayer;
    }

    public bool getWaitingForPlayer()
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

    public void setWaitingForPlayerToThrowStick(bool value)
    {
        if(value == true)
        {
            fetchState = FetchState.waitingForPlayerToThrowStick;
        }
    }
}
