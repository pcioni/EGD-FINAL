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
	public bool facePlayer = false;
	public TextControl textController;
    protected StringParser stringParser;
    public string[] dialogueArray;
	bool in_range_to_talk = false;
	public bool sprite_starts_left = true;
	GameObject main_character;
	int counter = 0;
	public bool is_object;
	private GameObject interactable_particles;

    private string currentDialogue;
    private bool interrupted;

    void Awake()
    {
        checkPrefab();
        dialogueIndex = 0;
        stringParser = new StringParser();
		main_character = GameObject.FindGameObjectWithTag ("Player");
    }

	void Update(){
		if (idle) {
			Idle ();
		}

		if (in_range_to_talk && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.Return))) {
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
		if (facePlayer) {
			FacePlayer ();
		}
		if (is_object) {
			interactable_particles = (GameObject) Instantiate (Resources.Load("Interactable Particles"));
		}

    }

    protected void OnTriggerExit2D(Collider2D other)
    {
		in_range_to_talk = false;
		if (facePlayer) {
			FacePlayer ();
		}
		if (interactable_particles != null) {
			Destroy (interactable_particles);
		}
    }

    /* Speaks dialogue from the dialogueArray
    *  Repeats the last entry in the dialogueArray once dialogue has been exhausted.
    *  Reads text until EOF or presence of flags.INTERRUPT in currentDialogue.
    */
    protected virtual void SpeakDialogue()
    {
		FacePlayer ();
		bool do_restore_idle = idle;
		idle = false;
        //Repeat the last line of dialogue once we've exhausted all the dialogue
        // i.e. "I'm done with you" -> "Go away..." -> "Go away..."
        if (dialogueIndex > dialogueArray.Length)
            dialogueIndex--;

        currentDialogue = dialogueArray[dialogueIndex];

        while (dialogueIndex < dialogueArray.Length) {
            string[] parseInfo = stringParser.ParseNameDialogueString(dialogueArray[dialogueIndex++]); //index++ indexes the array and then increments
            string speaker = parseInfo[0];
            string dialogue = parseInfo[1];

            textController.write(dialogue);

            if (stringParser.ContainsFlag(dialogue, flags.INTERRUPT)) {
                Debug.Log(string.Format("Text Interrupted: dialougeIndex = {0}, currentDialogue = {1}", dialogueIndex, dialogue));
                break;
            }
        }

		if (do_restore_idle)
			idle = true;
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

	void FacePlayer(){
		if (main_character.transform.position.x < transform.position.x ) {
			if (sprite_starts_left) {
				transform.eulerAngles = Vector3.zero;
			} 
			else {
				transform.eulerAngles = new Vector3 (0, 180, 0);
			}
		}
		else if (main_character.transform.position.x > transform.position.x ) {
			print ("Sam is to my right");
			if (!sprite_starts_left) {
				print ("turning toward Sam");
				transform.eulerAngles = Vector3.zero;
			} 
			else {
				transform.eulerAngles = new Vector3(0,180,0);
			}
		}

	}
}
