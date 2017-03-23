using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcheryLevelScript : MonoBehaviour {


    public float numTargets;
    private GameObject[] targets;
    private float numTargetsHit = 0f;
    private ReadSceneNames readSceneNamesBehaviour;
    private string[] sceneNames;

    // Use this for initialization
    void Start () {
        targets = GameObject.FindGameObjectsWithTag("ArcheryTarget");
        numTargets = targets.Length;

        readSceneNamesBehaviour = GetComponent<ReadSceneNames>();
        sceneNames = readSceneNamesBehaviour.getNames();
    }

    public void hitTarget()
    {
        //Targets will call this function if they get hit.
        numTargetsHit++;

        //If we have hit all the targets, success!
        if (numTargetsHit >= numTargets)
        {
            //Get next scene index
            int index = SceneManager.GetActiveScene().buildIndex + 1;

            //If we are not at the final level, Load the scene using SteamVR_LoadLevel
            if (index != 9)
            {
                SteamVR_LoadLevel.Begin(sceneNames[index], false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
            }
            else
            {
                //If we have completed all levels, go back to the hub
                SteamVR_LoadLevel.Begin("levelhub", false, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f);
            }
        }
    }
}
