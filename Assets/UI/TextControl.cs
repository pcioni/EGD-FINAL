using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

/// <summary>
/// A wrapper for the amazing UI text display!

/// TEXT FLAGS:

/// # -> newline 
/// "Hello there#...friend"
/// Hello There
/// ...friend

/// _ -> 1.5 second pause

/// ~ -> clears out textbox and writes the remainder in a new textbook

/// @ -> choice
/// "Where do you live? @(Here, There, Wither, Hither)"
/// Where do you live?
///   Here    There
///   Wither  Hither

/// % -> jump to index (appears after a choice)

/// $ -> interruption
/// Breaks the current readDialogue instance. Place this at the end of the string.

/// </summary


public class TextControl : MonoBehaviour {
	[TextArea]
	public string Notes;

	public GameObject choiceText; 
	public GameObject longText;
	public GameObject listText;
	public GameObject back;
	List<GameObject> buttons;
	GameObject first_button;
	public GameObject list_button_prefab; 
	public GameObject button_grid;
	public Scrollbar scrollbar;
	public GameObject battleManager;
	EventSystem event_system;

	// Use this for initialization
	void Start () {
		choiceText.SetActive (true);
		//longText = GameObject.Find("UI Long Text");
		buttons = new List<GameObject> ();
		buttons.Add (GameObject.Find ("Choice Button 1 Text"));
		buttons.Add (GameObject.Find ("Choice Button 2 Text"));
		buttons.Add (GameObject.Find ("Choice Button 3 Text"));
		buttons.Add (GameObject.Find ("Choice Button 4 Text"));
		first_button = GameObject.Find ("Choice Button 1");
		event_system = EventSystem.current.GetComponent<EventSystem> ();
		noText ();
	}

	///<summary>
	///A single argument tells the UI that you want 
	/// no choices, just to display text in the whole box
	/// </summary>
	public void write(string s){
		if (!back.activeInHierarchy){
			back.SetActive (true);
		}

		if (choiceText.activeInHierarchy || listText.activeInHierarchy) {
			//switch over to longText
			choiceText.SetActive(false);
			listText.SetActive (false);
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
	public void write(string s, List<string> b, string speaker = null){
		if (!back.activeInHierarchy){
			back.SetActive (true);
		}

		if (b.Count == 0)
			return;

		if (b.Count <= 4) {
			if (longText.activeInHierarchy || listText.activeInHierarchy) {
				//switch over to choiceText
				longText.SetActive (false);
				listText.SetActive (false);
				choiceText.SetActive (true);
			}
			choiceText.GetComponent<typeMessage> ().SetMessage (s, true);

			for (int x = 0; x < 4; x++) {
				if (x >= b.Count) {
					buttons [x].SetActive (false);
					continue;
				}
				buttons [x].SetActive (true);
				buttons [x].GetComponent<Text> ().text = b [x];
			}
			first_button.GetComponent<Button> ().Select ();
		} 
		else {
			if (longText.activeInHierarchy || choiceText.activeInHierarchy) {
				//switch over to choiceText
				longText.SetActive (false);
				choiceText.SetActive (false);
				listText.SetActive (true);
			}
			listText.GetComponent<typeMessage> ().SetMessage (s, true);

			//clear pre-existing buttons
			foreach (Button button in button_grid.GetComponentsInChildren<Button>()) {
				Destroy (button.gameObject);
			}

			for (int x = 0; x < b.Count; x++) {
				GameObject new_button = Instantiate (list_button_prefab);
				new_button.transform.SetParent (button_grid.transform);
				new_button.GetComponent<Text> ().text = b [x];
				new_button.transform.localScale = Vector3.one;
				new_button.GetComponent<Button> ().onClick.AddListener (OnClickSendNumber); //ONCLIKGIVESNUMBER
			}
			scrollbar.value = 1;
			button_grid.GetComponentsInChildren<Button> ()[0].Select ();
		}

	}

	void OnClickSendNumber(){
		GameObject button = event_system.currentSelectedGameObject;
		int loc = GameObject.Find ("Button List Grid").GetComponentsInChildren<Button> ().ToList ().IndexOf (button.GetComponent<Button> ())+1;
		battleManager.GetComponent<BattleManager> ().ReceiveButtonSignal (loc.ToString());
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
