using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {
    /*
     * Interactable is a base class for all interactable objects in the world. 
     * An intractable object is any object that has a consequence for player interaction, be it movement or an "interact" hotkey.
     * Any object in this category that needs a custom version of OnTriggerEnter, SpeakDialogue, or PostDialogueAction should inherit from this class.
     * 
     * Keep in mind that the base OnTriggerEnter function present here may be sufficient (Speaking dialogue and then performing a stock post-dialogue action).
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
    protected int dialogueIndex;
    protected Sprite sprite;
    protected TextControl textController;
    protected StringParser stringParser;
    public string[] dialogueArray;

    private string currentDialogue;
    private bool interrupted;

    

    void Awake() {
        checkPrefab();
        dialogueIndex = 0;
        stringParser = new StringParser();
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

    /* Speaks dialogue from the dialogueArray
    *  Repeats the last entry in the dialogueArray once dialogue has been exhausted.
    *  Reads text until EOF or presence of flags.INTERRUPT in currentDialogue.
    */
    protected virtual void SpeakDialogue() {

        //Repeat the last line of dialogue once we've exhausted all the dialogue
        // i.e. "I'm done with you" -> "Go away..." -> "Go away..."
        if (dialogueIndex > dialogueArray.Length)
            dialogueIndex--;

        interrupted = false;
        currentDialogue = dialogueArray[dialogueIndex];

        while (dialogueIndex < dialogueArray.Length || interrupted == false) {
            if (stringParser.ContainsFlag(currentDialogue, flags.INTERRUPT)) {
                interrupted = true;
                Debug.Log(string.Format("Text Interrupted: dialougeIndex = {0}, currentDialogue = {1}", dialogueIndex, currentDialogue));
            }

            textController.write(dialogueArray[dialogueIndex++]); //index++ indexes the array and then increments
            currentDialogue = dialogueArray[dialogueIndex];
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
