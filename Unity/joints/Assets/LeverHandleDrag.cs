using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandleDrag : MonoBehaviour {

    /*
     * Player can drag the lever handle to move the lever
     */

    //The z axis point from world to screen co-ordinates
    private Vector3 screenPoint;

    void OnMouseDown()
    {
        //Get initial screen point position, we can then calculate the drag offset using this
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnMouseDrag()
    {
        //Get the screen point from world coordinates
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        //Convert the offsetted position back to world point assign the new position
        transform.position = Camera.main.ScreenToWorldPoint(curScreenPoint);
    }
}
