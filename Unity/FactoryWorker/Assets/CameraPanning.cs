using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning : MonoBehaviour {

    public float radius = 5f;
    public float currentRotation = 0f;
    public Transform playerHeadPos;
    private Quaternion rotation;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += Time.deltaTime * 100;
        rotation.eulerAngles = new Vector3(0, currentRotation, 0) + playerHeadPos.position;
        transform.position = rotation.eulerAngles * radius;
    }
}
