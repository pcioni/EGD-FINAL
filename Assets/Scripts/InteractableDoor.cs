using UnityEngine;
using System.Collections;

public class InteractableDoor : Interactable {

    public Transform targetDest;
    private GameObject player;

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
        isTriggered = true;
        player = other.gameObject;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
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
        Debug.Log("Teleporting player...");
        player.transform.position = targetDest.position;
        player.GetComponent<CharacterController>().ClampToGround();
    }



}
