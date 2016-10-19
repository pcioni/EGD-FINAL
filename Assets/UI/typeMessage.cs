using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class typeMessage : MonoBehaviour {
	private string message = "";
	[TextArea]
	public string Notes;

	public string input = "";
	float pause_seconds = 1.0f;
	float letter_seconds = .04f;

	// Use this for initialization
	void Start () {
		//SetMessage (input);
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator writeMessage(){
		string text = GetComponent<Text> ().text;
		string add_text = message [GetComponent<Text>().text.Length].ToString ();
		//special conditions
		if (add_text == "#")
			add_text = "\n";
		if (add_text == "_") {
			add_text = "";
			int indexOf = message.IndexOf ('_');
			message = message.Remove(indexOf, 1);
			if (!Input.GetKey (KeyCode.RightShift))
				yield return new WaitForSeconds (pause_seconds);
		}
		if (add_text == "~") {
			print (message.Substring (text.Length));
			SetMessage (message.Substring (text.Length+1));
			return true;
		}
		GetComponent<Text> ().text = text + add_text;
		if (!Input.GetKey(KeyCode.RightShift)) yield return new WaitForSeconds (letter_seconds);
		if (GetComponent<Text> ().text.Length < message.Length)
			StartCoroutine ("writeMessage");
	}

	public void SetMessage(string new_message){
		StopCoroutine ("writeMessage");
		message = new_message;
		GetComponent<Text> ().text = "";
		StartCoroutine ("writeMessage");
	}
}
