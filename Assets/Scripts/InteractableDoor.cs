using UnityEngine;
using System.Collections;

public class InteractableDoor : Interactable {

    public Transform targetDest;
	public Camera destCamera;
	public Camera myCamera;
	public GameObject[] backgrounds;
    private GameObject player;
	public bool automatic;
	public bool just_received_player;

    void Awake() {
        checkPrefab();
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
		print ("Trigger on");
		isTriggered = true;
        player = other.gameObject;
		if (automatic && !just_received_player) {
			Teleport ();
		}
		just_received_player = false;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
		print ("Trigger off");
        isTriggered = false;
        player = null;
    }

    private void FixedUpdate()
    {
        if (isTriggered && Input.GetKeyDown(KeyCode.Space))
        {
            Teleport();
        }
    }

    //set player transform to target transform.
    private void Teleport()
    {
        //Debug.Log("Teleporting player...");
        player.transform.position = targetDest.position;
		if (destCamera != null) {
			myCamera.enabled = false;
			destCamera.enabled = true;
		}
		foreach(GameObject b in backgrounds){
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
