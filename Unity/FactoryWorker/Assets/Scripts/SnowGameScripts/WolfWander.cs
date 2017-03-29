using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfWander : MonoBehaviour {

    //The nav mesh agent
    private NavMeshAgent navMeshAgent;

    //The distance the agent will wander
    public float maxDistance = 10f;

    // Use this for initialization
    void Start () {

        //Get nav mesh agent
        navMeshAgent = GetComponent<NavMeshAgent>();

        //Set the destination to random location on the nav mesh
        Vector3 randomDirection = Random.insideUnitSphere;
        navMeshAgent.SetDestination(RandomNavSphere(transform.position, 20f, -1));
    }
	
	// Update is called once per frame
	void Update () {

        //have we reached the end of the path?
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude < 20f)
                {
                    navMeshAgent.SetDestination(RandomNavSphere(transform.position, 40f, -1));
                }
            }
        }
    }

    private Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        //Get a random point on the nav mesh

        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
