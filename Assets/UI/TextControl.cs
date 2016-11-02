using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// A wrapper for the amazing UI text display!
/// </summary>
public class TextControl : MonoBehaviour {
	[TextArea]
	public string Notes;

	public GameObject choiceText; 
	public GameObject longText;
	public GameObject back;
	List<GameObject> buttons;
	GameObject first_button;

	// Use this for initialization
	void Start () {
		choiceText = GameObject.Find ("UI Choice Text");
		//longText = GameObject.Find("UI Long Text");
		back = GameObject.Find ("Text Background");
		buttons = new List<GameObject> ();
		buttons.Add (GameObject.Find ("Choice Button 1 Text"));
		buttons.Add (GameObject.Find ("Choice Button 2 Text"));
		buttons.Add (GameObject.Find ("Choice Button 3 Text"));
		buttons.Add (GameObject.Find ("Choice Button 4 Text"));
		first_button = GameObject.Find ("Choice Button 1");
	}

	///<summary>
	///A single argument tells the UI that you want 
	/// no choices, just to display text in the whole box
	/// </summary>
	public void write(string s){
		if (!back.activeInHierarchy){
			back.SetActive (true);
		}

		if (choiceText.activeInHierarchy) {
			//switch over to longText
			choiceText.SetActive(false);
			longText.SetActive (true);
		}
		longText.GetComponent<typeMessage> ().SetMessage (s, false);
	}

	///<summary>
	/// b: Leave this empty to leave the buttons unchanged.
	/// The first string signifies what goes in the upper textbox.
	/// If the string array is not empty, the button texts are changed to
	/// the contents of the array. 
	/// </summary>
	public void write(string s, List<string> b){
		if (!back.activeInHierarchy){
			back.SetActive (true);
		}

		if (longText.activeInHierarchy) {
			//switch over to longText
			longText.SetActive(false);
			choiceText.SetActive (true);
		}
		choiceText.GetComponent<typeMessage> ().SetMessage (s, true);
		if (b.Count != 4) {
			if (b.Count != 0)
				print ("ALERT: b is not of length 4");
			return;
		}
		for (int x = 0; x < 4; x++) {
			buttons [x].GetComponent<Text> ().text = b [x];
		}
		first_button.GetComponent<Button> ().Select ();
	}

	/// <summary>
	/// Gets rid of the textbox completely. 
	/// Calling write brings it back.
	/// </summary>
	public void noText(){
		if (back.activeInHierarchy){
			back.SetActive (false);
		}
	}

	public bool waitForSpace(){
		return longText.activeSelf;
	}
		
}
