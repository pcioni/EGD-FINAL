using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	List<GameObject> good_guys;
	List<GameObject> bad_guys;
	List<GameObject> participants;
	string state;
	int picker;
	int picker2;
	bool awaiting_input;
	bool victory;
	bool defeat;
	bool continuer;
	public bool message_finished;
	char need_target;
	List<GameObject> pending_actions;
	List<string> pending_messages;
	TextControl text_controller;
	List<string> buttons_pressed;

	// Use this for initialization
	void Start () {
		state = "not started";
		good_guys = new List<GameObject> ();
		bad_guys = new List<GameObject> ();
		participants = new List<GameObject> ();
		StartBattle (new List<string>{ "Sam", "Amelia", "Nico", "Sam Gold" }, new List<string>{ "Manticore", "Slime", "Manticore" });
		awaiting_input = false;
		victory = false;
		defeat = false;
		continuer = false;
		need_target = 'n';
		pending_actions = new List<GameObject> ();
		pending_messages = new List<string> ();
		buttons_pressed = new List<string> ();
		text_controller = GameObject.Find ("Text Controller").GetComponent<TextControl> ();
	}

	public List<GameObject> getGoodGuys(){
		return good_guys;
	}

	public List<GameObject> getBadGuys(){
		return bad_guys;
	}

	public void SendMessagey(string message){
		text_controller.write (message);
		awaiting_input = true;
		message_finished = false;
	}

	public void NeedTargeting(char which){
		need_target = which;
	}
	
	// Update is called once per frame
	void Update () {

		if (awaiting_input) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (message_finished) {
					awaiting_input = false;
				} 
				else {
					FindObjectOfType<typeMessage> ().skip = true;
				}
			}
			return;
		}
		if (pending_actions.Count > 0) {
			pending_messages.AddRange(pending_actions [0].GetComponent<FightBehavior> ().doAction ());
			pending_actions.RemoveAt (0);
			return;
		}
		if (pending_messages.Count > 0) {
			SendMessagey (pending_messages [0]);
			pending_messages.RemoveAt (0);
			return;
		}

		if (defeat) {
			pending_messages.Add ("You have been defeated... Game Over!");
			return;
		}

		switch (state) {

		case ("not started"):
			return;

		case ("instantiated"):
			SendMessagey("A battle has begun!");
			picker = -1;
			state = "pick actions";
			return;

		case ("pick actions"):
			if (picker == -1) {
				picker++;
				pending_messages.Add ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				if (picker > 0) {
					picker--;
					pending_messages.Add ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
				}
			}
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.G) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.H)) {
				continuer = true;
				if (Input.GetKeyDown (KeyCode.A)) {
					pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("attacks"));
				} else if (Input.GetKeyDown (KeyCode.G)) {
					pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("guards"));
				} else if (Input.GetKeyDown (KeyCode.V)) {
					pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("insta-kill"));
				} else if (Input.GetKeyDown (KeyCode.H)) {
					pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("hail-mary"));
				}
				if (need_target == 'e') {
					state = "select enemy";
					picker2 = -1;
					return;
				} else if (need_target == 'a') {
					picker2 = -1;
					state = "select teammate";
					return;
				}
			}
			if (continuer && !awaiting_input) {
				continuer = false;
				picker++;
				if (picker >= good_guys.Count) {
					SendMessagey ("All actions have been selected! Press space to fight!");
					state = "commence";
					picker = 0;
					return;
				}
				pending_messages.Add ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			return;

		case ("select enemy"):
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				state = "pick actions";
				continuer = false;
				pending_messages.Add ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (picker2 == -1) {
				picker2++;
				pending_messages.Add ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
			} else {
				if (Input.GetKeyDown (KeyCode.W)) {
					picker2--;
					if (picker2 < 0) {
						picker2 = bad_guys.Count - 1;
					}
					pending_messages.Add ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.S)) {
					picker2++;
					if (picker2 >= bad_guys.Count) {
						picker2 = 0;
					}
					pending_messages.Add ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.Space)) {
					SendMessagey (good_guys [picker].name + " will target " + bad_guys [picker2].name + "!");
					good_guys [picker].GetComponent<FightBehavior> ().setTarget (bad_guys [picker2]);
					state = "pick actions";
				}
			}
			return;

		case ("select teammate"):
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				state = "pick actions";
				continuer = false;
				pending_messages.Add ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (picker2 == -1) {
				picker2++;
				pending_messages.Add ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
			} else {
				if (Input.GetKeyDown (KeyCode.W)) {
					picker2--;
					if (picker2 < 0) {
						picker2 = good_guys.Count - 1;
					}
					pending_messages.Add ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.S)) {
					picker2++;
					if (picker2 >= good_guys.Count) {
						picker2 = 0;
					}
					pending_messages.Add ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.Space)) {
					pending_messages.Add (good_guys [picker].name + " will target " + good_guys [picker2].name  + "!");
					good_guys [picker].GetComponent<FightBehavior> ().setTarget (good_guys [picker2]);
					state = "pick actions";
				}
			}
			return;

		case ("commence"):
			if (continuer){
				continuer = false;
				if (victory) {
					picker = participants.Count;
				}
				picker++;
			} 
			if (picker < participants.Count) {
				while (!participants [picker].activeSelf) {
					picker++;
					if (picker >= participants.Count) {
						return;
					}
				}
				pending_actions.Add (participants [picker]);
				continuer = true;
			}
			else {
				picker = 0;
				state = "end turn";
			}
			return;

		case ("end turn"):
			if (continuer) {
				continuer = false;
				if (victory) {
					picker = participants.Count;
				}
				picker++;
			}
			if (picker < participants.Count) {
				while (!participants [picker].activeSelf) {
					picker++;
					if (picker >= participants.Count) {
						return;
					}
				}
				pending_messages.AddRange (participants [picker].GetComponent<FightBehavior> ().endTurn ());
				continuer = true;
			} else {
				picker = 0;
				state = "check win";
				if (!victory) {
					pending_messages.Add ("The turn has ended. Press space to continue.");
				}
			}
			return;

		case ("check win"):
			if (victory) {
				pending_messages.Add ("Congratulations, you have won!");
				state = "";
			} else {
				state = "pick actions";
				picker = -1;
			}
			return;

		default:
			return;
		}
	}

	void StartBattle(List<string> good, List<string> bad){
		for (int x = 0; x < good.Count; x++) {
			GameObject temp = (GameObject)Instantiate (Resources.Load (good [x]), Vector3.zero, Quaternion.identity);
			temp.GetComponent<FightBehavior> ().setAlignment (true);
			good_guys.Add (temp);
			participants.Add (temp);
		}
		for (int x = 0; x < bad.Count; x++) {
			GameObject temp = (GameObject)Instantiate (Resources.Load (bad [x]), Vector3.zero, Quaternion.identity);
			temp.GetComponent<FightBehavior> ().setAlignment (false);
			bad_guys.Add (temp);
			participants.Add (temp);
		}
		state = "instantiated";
		List<PositionCharactersInBattle> tempy = new List<PositionCharactersInBattle> ();
		tempy.AddRange (FindObjectsOfType<PositionCharactersInBattle> ());
		foreach (PositionCharactersInBattle item in tempy) {
			item.ArrangeCharacters (good_guys, bad_guys);
		}
	}

	public void kill(GameObject which){
		which.SetActive (false);
		good_guys.Remove (which);
		bad_guys.Remove (which);
		if (good_guys.Count == 0) {
			defeat = true;
		} else if (bad_guys.Count == 0) {
			victory = true;
		}
	}
			

	public void newTarget(GameObject which, bool good){
		if (good) {
			which.GetComponent<FightBehavior>().setTarget(bad_guys [Random.Range (0, bad_guys.Count)]);
		} else {
			which.GetComponent<FightBehavior>().setTarget(good_guys [Random.Range (0, good_guys.Count)]);
		}
	}

	public void ReceiveButtonSignal(string button_name){
		buttons_pressed.Add (button_name);
	}
}
