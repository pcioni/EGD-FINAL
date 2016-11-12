using UnityEngine;
using System.Collections;

public class InteractableSpeaker : Interactable {

    /*
     * Any object in this category that needs a custom version of SpeakDialogue, or PostDialogueAction should inherit from this class.
     * Keep in mind that the base OnTriggerEnter function present here may be sufficient (Speaking dialogue and then performing a stock post-dialogue action).
     * 
     * Virtual functions listed here:
     *      OnTriggerEnter
     *      SpeakDialogue
     *      PostDialogueAction
     *      
     * An Interactable object MUST HAVE:
     *      BoxCollider2D
     *      Sprite
     * CheckPrefab attempts to manually add them to avoid nullref errors, but cannot guarantee completeness.
     */


    protected int dialogueIndex;
	protected SpriteRenderer sprite_renderer;
	public bool idle;
	public TextControl textController;
    protected StringParser stringParser;
    public string[] dialogueArray;
	bool in_range_to_talk = false;

    private string currentDialogue;
    private bool interrupted;

    void Awake()
    {
        checkPrefab();
        dialogueIndex = 0;
        stringParser = new StringParser();
    }

	void Update(){
		if (idle) {
			Idle ();
		}

		if (in_range_to_talk && Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return)) {
			//TEMPORARY
			if (textController.back.activeInHierarchy)//temp
				textController.noText ();//temp
			else//temp
				SpeakDialogue ();
		}
	}

    //ensures the interactable has all the required components
    protected override void checkPrefab()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.Log(string.Format("No BoxCollider2D attached to Interactable object {0} -- adding one manually", name));
            boxCollider = addBoxCollider2D();
        }

		textController = GameObject.Find("TextControl").GetComponent<TextControl>();
        if (textController == null)
        {
            Debug.Log(string.Format("No TextControl attached to Interactable object {0} -- adding one manually", name));
            textController = gameObject.AddComponent<TextControl>() as TextControl;
        }

        sprite_renderer = GetComponent<SpriteRenderer>();
        if (sprite_renderer == null)
        {
            Debug.LogError(string.Format("No SpriteRenderer attached to Interactable object {0}", name));
            Debug.Break();
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
		in_range_to_talk = true;
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
		in_range_to_talk = false;
    }

    /* Speaks dialogue from the dialogueArray
    *  Repeats the last entry in the dialogueArray once dialogue has been exhausted.
    *  Reads text until EOF or presence of flags.INTERRUPT in currentDialogue.
    */
    protected virtual void SpeakDialogue()
    {

        //Repeat the last line of dialogue once we've exhausted all the dialogue
        // i.e. "I'm done with you" -> "Go away..." -> "Go away..."
        if (dialogueIndex > dialogueArray.Length)
            dialogueIndex--;

        interrupted = false;
        currentDialogue = dialogueArray[dialogueIndex];

        while (dialogueIndex < dialogueArray.Length || interrupted == false)
        {
            if (stringParser.ContainsFlag(currentDialogue, flags.INTERRUPT))
            {
                interrupted = true;
                Debug.Log(string.Format("Text Interrupted: dialougeIndex = {0}, currentDialogue = {1}", dialogueIndex, currentDialogue));
            }

            textController.write(dialogueArray[dialogueIndex++]); //index++ indexes the array and then increments
            currentDialogue = dialogueArray[dialogueIndex];
        }
    }

    protected virtual void PostDialogueAction()
    {

    }

    protected override BoxCollider2D addBoxCollider2D()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        boxCollider.isTrigger = true;
		boxCollider.size = new Vector2(sprite_renderer.sprite.border.x + 50, sprite_renderer.sprite.border.y); //extend collider outside the sprite for onTrigger collisions.
        return boxCollider;
    }

	void Idle(){
		int flip_chance = 300;
		if (Random.Range (0, flip_chance) == 0) {
			//one in flip_chance possiblity that 
			//the character will turn around
			transform.Rotate(new Vector3(0,180,0));

		}
	}
}
