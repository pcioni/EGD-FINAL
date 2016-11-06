using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    /*
     * Interactable is a base class for all interactable objects in the world. 
     * An intractable object is any object that has a consequence for player interaction, be it movement or an "interact" hotkey.
     * Any object in this category that needs a custom version of OnTriggerEnter, SpeakDialogue, or PostDialogueAction should inherit from this class.
     * 
     * Keep in mind that the base OnTriggerEnter function present here may be sufficient (Speaking dialogue and then performing a post-dialogue action).
     * 
     * Virtual functions listed here:
     *      OnTriggerEnter
     *      SpeakDialogue
     *      PostDialogueAction
     */ 


    protected BoxCollider2D boxCollider;
    protected int dialogueIndex;
    protected Sprite sprite;
    protected TextControl textController;
    public string[] dialogueArray;

    

    void Awake() {
        checkPrefab();
        dialogueIndex = 0; //-1 to assert null values
    }

    //ensures the interactable has all the required components
    private void checkPrefab() {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null) {
            Debug.Log("No BoxCollider2D attached to Interactable object -- adding one manually");
            boxCollider = addBoxCollider2D();
        }

        textController = GetComponent<TextControl>();
        if (textController == null) {
            Debug.Log("No TextControl attached to Interactable object -- adding one manually");
            textController = gameObject.AddComponent<TextControl>() as TextControl;
        }

        sprite = GetComponent<Sprite>();
        if (sprite == null) {
            Debug.LogError("No Sprite attached to Interactable object");
            Debug.Break();
        }
    }

    protected virtual void OnTriggerEnter(Collider other) {

    }

	void Start () {
	
	}
	
    void FixedUpdate() {

    }

	void Update () {
	
	}

    /*
     * Reads the next numToRead strings in the dialogueArray 
     */ 
    protected virtual void SpeakDialogue(int numToRead) {
        for (;numToRead > 0; numToRead--) {
            if (dialogueIndex > dialogueArray.Length) {
                Debug.LogError("dialogueIndex > dialougeArray.length");
                Debug.Break();
            }
            textController.write(dialogueArray[dialogueIndex++]); //index++ indexes the array and then increments
        }
    }

    protected virtual void PostDialogueAction() {

    }

    private BoxCollider2D addBoxCollider2D() {
        boxCollider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(sprite.border.x + 50, sprite.border.y); //extend collider outside the sprite for onTrigger collisions.
        return boxCollider;
    }
}
