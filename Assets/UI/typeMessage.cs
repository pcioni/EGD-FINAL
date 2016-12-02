using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class typeMessage : MonoBehaviour {
	private string message = "";
	public bool finished_writing = false;
	[TextArea]
	public string Notes;

	public string input = "";
	float pause_seconds = 1.0f;
	float original_pause_seconds;
	float letter_seconds = .02f;
	float original_letter_seconds;
	Text text_component;
	public bool skip = false;

	// Use this for initialization
	void Start () {
		original_letter_seconds = letter_seconds;
		original_pause_seconds = pause_seconds;
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator writeMessage(){
		int count = 0;
		text_component = GetComponent<Text> ();
		skip = false;
		while (count < message.Length){

			if (skip) {
				skip = false;
				text_component.text = message;
				break;
			}

			char next_char = message[count];
			//special conditions
			if (next_char == '#')
				next_char = '\n';
			if (next_char == '_') {
				yield return new WaitForSeconds (pause_seconds);
				count++;
				continue;
			}
			if (next_char == '~') {
				text_component.text = "";
				count++;
				continue;
			}
			if (next_char == '$') {
				count++;
				continue;
			}
			text_component.text += next_char;
			yield return new WaitForSeconds (letter_seconds);
			count++;
		}
		finished_writing = true;
		if (FindObjectOfType<BattleManager> ())
			FindObjectOfType<BattleManager> ().message_finished = true;
	}

	public void SetMessage(string new_message, bool instant){
		finished_writing = false;
		if (!instant) {
			StopCoroutine ("writeMessage");
			message = new_message;
			GetComponent<Text> ().text = "";
			StartCoroutine ("writeMessage");
		} else {
			message = new_message;
			GetComponent<Text> ().text = new_message;
		}
	}

	public void SpeedText(){
		letter_seconds = 0;
		pause_seconds = 0;
	}

	public void UnspeedText(){
		letter_seconds = original_letter_seconds;
		pause_seconds = original_pause_seconds;
	}
		
}
