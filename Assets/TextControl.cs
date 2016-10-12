using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// A wrapper for the amazing UI text display!
/// </summary>
public class TextControl : MonoBehaviour {
	[TextArea]
	public string Notes;

	public GameObject choiceText; 
	public GameObject longText;

	//TEST
	[Header("Test: Q- place input in textbox, W- title buttons as well")]
	public string input;
	public string[] input_array;

	// Use this for initialization
	void Start () {
		choiceText = GameObject.Find ("UI Choice Text");
		//longText = GameObject.Find("UI Long Text");

	}
	
	// Update is called once per frame
	void Update () {
		//TEST
		if (Input.GetKeyDown (KeyCode.Q)) {
			write (input);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			write (input, input_array);
		}
	}

	///<summary>
	///A single argument tells the UI that you want 
	/// no choices, just to display text in the whole box
	/// </summary>
	public void write(string s){
		if (choiceText.activeInHierarchy) {
			//switch over to longText
			choiceText.SetActive(false);
			longText.SetActive (true);
		}
		longText.GetComponent<typeMessage> ().SetMessage (s);
	}

	///<summary>
	/// b: Leave this empty to leave the buttons unchanged.
	/// The first string signifies what goes in the upper textbox.
	/// If the string array is not empty, the button texts are changed to
	/// the contents of the array. 
	/// </summary>
	public void write(string s, string[] b){
		if (longText.activeInHierarchy) {
			//switch over to longText
			longText.SetActive(false);
			choiceText.SetActive (true);
		}
		choiceText.GetComponent<typeMessage> ().SetMessage (s);
		if (b.Length != 4) {
			if (b.Length != 0)
				print ("ALERT: b is not of length 4");
			return;
		}
		GameObject.Find ("Choice Button 1 Text").GetComponent<Text> ().text = b [0];
		GameObject.Find ("Choice Button 2 Text").GetComponent<Text> ().text = b [1];
		GameObject.Find ("Choice Button 3 Text").GetComponent<Text> ().text = b [2];
		GameObject.Find ("Choice Button 4 Text").GetComponent<Text> ().text = b [3];
	}
		
}
