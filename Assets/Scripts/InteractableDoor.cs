﻿using UnityEngine;
using System.Collections;

public class InteractableDoor : Interactable {

    public Transform targetDest;
	Camera destCamera;
	Camera myCamera;
	GameObject[] backgrounds;
    private GameObject player;
	public bool automatic;
	bool just_received_player;
    private bool isTriggered = false;

    void Awake() {
        checkPrefab();
		myCamera = transform.parent.GetComponentInChildren<Camera> ();
		destCamera = targetDest.transform.parent.GetComponentInChildren<Camera> ();
		FitBackgroundToCamera[] fit_array = targetDest.transform.parent.GetComponentsInChildren<FitBackgroundToCamera> ();
		backgrounds = new GameObject[fit_array.Length];
		int i = 0;
		foreach (FitBackgroundToCamera f in fit_array) {
			backgrounds [i] = f.gameObject;
			i++;
		}
	}

    //ensures the interactable has all the required components
    protected override void checkPrefab() {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null) {
            Debug.Log(string.Format("No BoxCollider2D attached to Interactable object {0} -- adding one manually", name));
            boxCollider = addBoxCollider2D();
        }

        if (targetDest == null)
            Debug.Log(string.Format("Door {0} does not have a target destination", name));
    }

    protected void OnTriggerEnter2D(Collider2D other) {
		//print ("Trigger on");
		isTriggered = true;
        player = other.gameObject;
		if (automatic && !just_received_player) {
			Teleport ();
		}
		just_received_player = false;
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
		//print ("Trigger off");
        isTriggered = false;
        player = null;
    }

    private void Update()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.Space))
            Teleport();
    }

    //set player transform to target transform.
    private void Teleport()
    {
        //Debug.Log("Teleporting player...");
        Vector2 correctedHeight = new Vector2(targetDest.position.x, targetDest.position.y + boxCollider.bounds.size.y);
        player.transform.position = correctedHeight;
		if (destCamera != null) {
			myCamera.enabled = false;
			//print ("enabling " + destCamera);
			destCamera.enabled = true;
		}
        else 
            destCamera = targetDest.transform.parent.GetComponentInChildren<Camera>();
        foreach (GameObject b in backgrounds){
			b.GetComponent<FitBackgroundToCamera> ().
			Align (destCamera.gameObject.transform);
		}
		player.GetComponent<CharacterController>().ClampToGround();
		InteractableDoor id = targetDest.GetComponent<InteractableDoor> ();
		if (id) {
			id.just_received_player = true;
		}
    }


}
