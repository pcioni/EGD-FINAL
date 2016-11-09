using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    /*
     * Interactable is a base class for all interactable objects in the world. 
     * An intractable object is any object that has a consequence for player interaction, be it movement or an "interact" hotkey.
     * 
     * Virtual functions listed here:
     *      OnTriggerEnter
     *      SpeakDialogue
     *      PostDialogueAction
     *      
     * An Interactable object MUST HAVE:
     *      BoxCollider2D
     *      TextController
     *      Sprite
     * CheckPrefab attempts to manually add them to avoid nullref errors, but cannot guarantee completeness.
     */ 


    protected BoxCollider2D boxCollider;
    protected bool isTriggered = false;

    void Awake() {
        checkPrefab();
    }

    //ensures the interactable has all the required components
    protected virtual void checkPrefab() {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null) {
            Debug.Log("No BoxCollider2D attached to Interactable object -- adding one manually");
            boxCollider = addBoxCollider2D();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        isTriggered = false;
    }

    protected virtual BoxCollider2D addBoxCollider2D() {
        boxCollider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(50, 50); //extend collider outside the sprite for onTrigger collisions.
        return boxCollider;
    }
}
