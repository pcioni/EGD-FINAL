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
	char need_target;
	List<GameObject> pending_actions;
	List<string> pending_messages;
	TextControl text_controller;

	// Use this for initialization
	void Start () {
		state = "not started";
		good_guys = new List<GameObject> ();
		bad_guys = new List<GameObject> ();
		participants = new List<GameObject> ();
		StartBattle (new List<string>{ "Red", "Green", "Blue" }, new List<string>{ "Blue", "Blue", "Red", "Red" });
		awaiting_input = false;
		victory = false;
		defeat = false;
		continuer = false;
		need_target = 'n';
		pending_actions = new List<GameObject> ();
		pending_messages = new List<string> ();
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
	}

	public void NeedTargeting(char which){
		need_target = which;
	}
	
	// Update is called once per frame
	void Update () {

		if (awaiting_input) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				awaiting_input = false;
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
				text_controller.write ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				if (picker > 0) {
					picker--;
					text_controller.write ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
				}
			}
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.G) || Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown(KeyCode.H)) {
				continuer = true;
				if (Input.GetKeyDown (KeyCode.A)) {
					text_controller.write (good_guys [picker].GetComponent<FightBehavior> ().setAction ("attacks"));
				} else if (Input.GetKeyDown (KeyCode.G)) {
					text_controller.write (good_guys [picker].GetComponent<FightBehavior> ().setAction ("guards"));
				} else if (Input.GetKeyDown (KeyCode.V)) {
					SendMessagey (good_guys [picker].GetComponent<FightBehavior> ().setAction ("insta-kill"));
				} else if (Input.GetKeyDown (KeyCode.H)) {
					SendMessagey (good_guys [picker].GetComponent<FightBehavior> ().setAction ("hail-mary"));
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
				text_controller.write ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			return;

		case ("select enemy"):
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				state = "pick actions";
				continuer = false;
				text_controller.write ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (picker2 == -1) {
				picker2++;
				text_controller.write ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
			} else {
				if (Input.GetKeyDown (KeyCode.W)) {
					picker2--;
					if (picker2 < 0) {
						picker2 = bad_guys.Count - 1;
					}
					text_controller.write ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.S)) {
					picker2++;
					if (picker2 >= bad_guys.Count) {
						picker2 = 0;
					}
					text_controller.write ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
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
				text_controller.write ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (picker2 == -1) {
				picker2++;
				text_controller.write ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
			} else {
				if (Input.GetKeyDown (KeyCode.W)) {
					picker2--;
					if (picker2 < 0) {
						picker2 = good_guys.Count - 1;
					}
					text_controller.write ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.S)) {
					picker2++;
					if (picker2 >= good_guys.Count) {
						picker2 = 0;
					}
					text_controller.write ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.Space)) {
					SendMessagey (good_guys [picker].name + " will target " + good_guys [picker2].name  + "!");
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
				state = "check win";
				if (!victory) {
					SendMessagey ("The turn has ended. Press space to continue.");
				}
			}
			return;

		case ("check win"):
			if (victory) {
				text_controller.write ("Congratulations, you have won!");
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
		Vector3 positiony = new Vector3 (-2f, 2f, 0f);
		for (int x = 0; x < good.Count; x++) {
			GameObject temp = (GameObject)Instantiate (Resources.Load (good [x]), positiony, Quaternion.identity);
			temp.GetComponent<FightBehavior> ().setAlignment (true);
			good_guys.Add (temp);
			participants.Add (temp);
			positiony += new Vector3 (0f, -1f, 0f);
		}
		positiony = new Vector3 (2f, 2f, 0f);
		for (int x = 0; x < bad.Count; x++) {
			GameObject temp = (GameObject)Instantiate (Resources.Load (bad [x]), positiony, Quaternion.identity);
			temp.GetComponent<FightBehavior> ().setAlignment (false);
			bad_guys.Add (temp);
			participants.Add (temp);
			positiony += new Vector3 (0f, -1f, 0f);
		}
		state = "instantiated";
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
}
