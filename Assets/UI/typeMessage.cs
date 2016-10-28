﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class typeMessage : MonoBehaviour {
	private string message = "";
	[TextArea]
	public string Notes;

	public string input = "";
	float pause_seconds = 1.0f;
	float letter_seconds = .04f;
	Text text_component;
	public bool skip = false;

	// Use this for initialization
	void Start () {
		//SetMessage (input);
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator writeMessage(){
		int count = 0;
		text_component = GetComponent<Text> ();
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
			text_component.text += next_char;
			yield return new WaitForSeconds (letter_seconds);
			count++;
		}
		FindObjectOfType<BattleManager> ().message_finished = true;
	}

	public void SetMessage(string new_message){
		StopCoroutine ("writeMessage");
		message = new_message;
		GetComponent<Text> ().text = "";
		StartCoroutine ("writeMessage");
	}
}
