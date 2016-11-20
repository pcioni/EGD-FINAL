using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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


    public string[] dialogueArray;
    protected int dialogueIndex;

    protected SpriteRenderer sprite_renderer;

	public bool idle;
	public bool facePlayer = false;

	public TextControl textController;
    protected StringParser stringParser;

	bool in_range_to_talk = false;
	public bool sprite_starts_left = true;
	GameObject main_character;
	public bool is_object;
	private GameObject interactable_particles;
	private bool do_sparkle = true;
    private bool isSpeaking = false;

    private bool isTriggered = false;

	public int new_progress_number = -1;
	public bool allowing_progress = false;
	public GameObject[] contingencies;//see ProgressLevel for explanation of contingencies

	public string give_item = "";
	public int item_quantity = 0;

    void Awake()
    {
        checkPrefab();
        dialogueIndex = 0;
        stringParser = new StringParser();
		main_character = GameObject.FindGameObjectWithTag ("Player");
		//check contingencies array for correctness
		foreach (GameObject obj in contingencies) {
			int their_num = obj.GetComponent<InteractableSpeaker> ().new_progress_number;
			if (their_num != new_progress_number) {
				print ("OBJECTION! New progress numbers in contingencies array must all match. " + their_num + " != " + new_progress_number);
			}
		}
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

    void FixedUpdate() {
        if (isTriggered && !isSpeaking && Input.GetKeyDown(KeyCode.Space)) {
            isSpeaking = true;
            StartCoroutine("SpeakDialogue");
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

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        isTriggered = true;
		in_range_to_talk = true;
		if (facePlayer) {
			FacePlayer ();
		}
		if (is_object && do_sparkle) {
			interactable_particles = (GameObject) Instantiate (Resources.Load("Interactable Particles"), transform.position, Quaternion.identity);
		}

    }

    protected virtual void OnTriggerExit2D(Collider2D other) {
        isTriggered = false;
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
    protected virtual IEnumerator SpeakDialogue() {
        isSpeaking = true;
		if (!is_object) FacePlayer ();
		bool do_restore_idle = idle;
		idle = false;
        //Repeat the last line of dialogue once we've exhausted all the dialogue
        // i.e. "I'm done with you" -> "Go away..." -> "Go away..."


        while (dialogueIndex < dialogueArray.Length) {
            string[] parseInfo = stringParser.ParseNameDialogueString(dialogueArray[dialogueIndex++]); //index++ indexes the array and then increments
            string speaker = parseInfo[0];
            string dialogue = parseInfo[1];
    
            Debug.Log("writing dialogue: " + dialogue);
            textController.write(dialogue);

            //TODO: feeds the data too fast, skipping dialogue lines
            yield return StartCoroutine(WaitForKeyDown(KeyCode.Space)); 

            if (stringParser.ContainsFlag(dialogue, flags.INTERRUPT)) {
                Debug.Log(string.Format("Text Interrupted: dialogueIndex = {0}, currentDialogue = {1}", dialogueIndex, dialogue));
                break;
            }
        }

        isSpeaking = false;

		if (do_restore_idle)
			idle = true;

		if (dialogueIndex == dialogueArray.Length) {
			do_sparkle = false;
			if (interactable_particles != null)
				Destroy (interactable_particles);
		}

		//finished obligatory conversation
		allowing_progress = true;
		if (new_progress_number != -1 && contingenciesAllow()) {
			GameObject.FindObjectOfType<ProgressLevel> ().updateOverworldProgress(new_progress_number);
		}

		if (give_item != "") {
			GameObject.FindObjectOfType<Information> ().addItemToInventory (give_item, item_quantity); 
		}
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode) {
        while (!Input.GetKeyDown(keyCode))
            yield return null;
    }

	bool contingenciesAllow(){
		foreach (GameObject obj in contingencies) {
			if (!obj.GetComponent<InteractableSpeaker> ().allowing_progress) {
				return false;
			}
		}
		return true;
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
