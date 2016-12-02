using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Epilogue : MonoBehaviour {
	TextControl text_controller;
	OverworldTextStorage text_storage;
	public GameObject long_text;

	string[] dialogueArray;
	int dialogueIndex = 0;

	Image fade_foreground;
	bool fading = false;

	AudioSource audioo;

	// Use this for initialization
	void Start () {
		text_controller = GameObject.FindObjectOfType<TextControl> ();
		text_storage = GameObject.FindObjectOfType<OverworldTextStorage> ();

		dialogueArray = text_storage.RetrieveDialogue ("epilogue");
		text_controller.write (dialogueArray [0]);

		fade_foreground = GameObject.Find ("Foreground").GetComponent<Image> ();
		audioo = GameObject.FindObjectOfType<AudioSource> ();
		print ("I am aware of the null ref, don't touch it!");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (!long_text.GetComponent<typeMessage> ().finished_writing) {
				long_text.GetComponent<typeMessage> ().SpeedText ();
				return;
			} else {
				long_text.GetComponent<typeMessage> ().UnspeedText ();
			}
			dialogueIndex++;
			if (dialogueIndex < dialogueArray.Length) {
				text_controller.write (dialogueArray [dialogueIndex]);
			} else {
				if (!fading) {
					StartCoroutine (FadeOut ());
					fading = true;
				}
			}
		}
	}

	IEnumerator FadeOut(){
		fade_foreground.enabled = true;
		while (fade_foreground.color.a < 1.0) {
			fade_foreground.color = new Color (
				fade_foreground.color.r,
				fade_foreground.color.g, 
				fade_foreground.color.b, 
				fade_foreground.color.a + .01f);
			audioo.volume = 1 - fade_foreground.color.a;
			yield return null;
		}
		SceneManager.LoadScene ("Main Menu");
	}
}
