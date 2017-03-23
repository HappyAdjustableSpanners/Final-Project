using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WandController : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } } 

	private HashSet<InteractableItem> objectsHoveringOver = new HashSet<InteractableItem> ();
	private InteractableItem closestItem;
	private InteractableItem interactingItem;
			
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent < SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null) {
			Debug.Log ("Controller is not initialised");
			return;
		}

		if (controller.GetPressDown(triggerButton) ) {
			float minDistance = float.MaxValue;
			float distance;
			closestItem = null;

			foreach (InteractableItem item in objectsHoveringOver) {
				distance = (item.transform.position - transform.position).sqrMagnitude; // could just use magnitude

				if (distance < minDistance) {
					minDistance = distance;
					closestItem = item;
				}
			}

			interactingItem = closestItem;

			if (interactingItem) {
				if (interactingItem.IsInteracting ()) {
					interactingItem.EndInteraction (this); //same as saying gameObject. It's more intuitive
				}
				interactingItem.BeginInteraction (this);
			}

		}

		if (controller.GetPressUp(triggerButton) && interactingItem != null ) {

			interactingItem.EndInteraction (this);
		}

        if (controller.GetPressDown(gripButton))
        {
            //Add grip button behaviour here
        }
	}

    private void OnTriggerEnter(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem != null)
        {
            if (collidedItem.enabled)
            {
                objectsHoveringOver.Add(collidedItem);


                //Highlight the item
                //Get renderers of item and its children
                Renderer[] renderers = collider.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.SetColor("_EmissionColor", new Color(0.7f, 0.7f, 0.7f, 1f));
                    DynamicGI.UpdateMaterials(r);
                }
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        InteractableItem collidedItem = collider.GetComponent<InteractableItem>();
        if (collidedItem != null)
        {
            if (collidedItem.enabled)
            {
                objectsHoveringOver.Remove(collidedItem);


                //Highlight the item
                //Get renderers of item and its children
                Renderer[] renderers = collider.gameObject.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.SetColor("_EmissionColor", Color.black);
                    DynamicGI.UpdateMaterials(r);
                }
            }
        }
    }
}
