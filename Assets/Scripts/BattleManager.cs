using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	List<GameObject> good_guys;
	List<GameObject> bad_guys;
	List<GameObject> participants;
	List<string> item_list;
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
	List<List<string>> pending_choices;
	TextControl text_controller;
	ItemBehavior inventory;
	int action_selected;

	// Use this for initialization
	void Start () {
		state = "not started";
		good_guys = new List<GameObject> ();
		bad_guys = new List<GameObject> ();
		participants = new List<GameObject> ();
		StartBattle (new List<string>{ "Sam", "Amelia", "Nico", "Cody" }, new List<string>{ "Manticore", "Slime", "Manticore" });
		awaiting_input = false;
		victory = false;
		defeat = false;
		continuer = false;
		need_target = 'n';
		pending_actions = new List<GameObject> ();
		pending_messages = new List<string> ();
		text_controller = GameObject.Find ("Text Controller").GetComponent<TextControl> ();
		pending_choices = new List<List<string>> ();
		item_list = new List<string> {
			"Pick an item to use!",
			"Potion",
			"Panacea Bottle",
			"Magic Lens",
			"The Kevin-Is-Awesome Bat"
		};
		inventory = GetComponent<ItemBehavior> ();
	}

	public List<GameObject> getGoodGuys(){
		return good_guys;
	}

	public List<GameObject> getBadGuys(){
		return bad_guys;
	}

	public string getItemName(int which){
		return item_list [which - 1];
	}

	public char itemNeedsTargeting(int which){
		if (which < 2) {
			return 'a';
		}
		else{ 
			return 'e'; 
		}
	}

	public string useItem(int which, GameObject user, GameObject target){
		return inventory.useItem(item_list [which - 1], user, target);
	}

	public void SendMessagey(string message){
		text_controller.write (message);
		awaiting_input = true;
		message_finished = false;
	}

	public void SendMessagey(List<string> message){
		string title = message [0];
		message.RemoveAt (0);
		text_controller.write (title, message);
		awaiting_input = true;
		message_finished = false;
		action_selected = 0;
	}

	public void NeedTargeting(char which){
		need_target = which;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Backspace)) {
			if (state == "pick actions") {
				if (picker > 0) {
					awaiting_input = false;
					picker--;
					pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
				}
			} else if (state.Split (' ') [0] == "select") {
				awaiting_input = false;
				state = "pick actions";
				continuer = false;
				pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
			}
		}

		if (awaiting_input) {
			if (text_controller.waitForSpace ()) {
				if (Input.GetKeyDown (KeyCode.Space)) {
					if (message_finished) {
						awaiting_input = false;
					} else {
						FindObjectOfType<typeMessage> ().skip = true;
						message_finished = true;
					}
				}
			} else if (Input.GetKeyDown (KeyCode.Space) && message_finished == false) {
				FindObjectOfType<typeMessage> ().skip = true;
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
		if (pending_choices.Count > 0) {
			SendMessagey (pending_choices [0]);
			pending_choices.RemoveAt (0);
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
			pending_messages.Add("A battle has begun!");
			picker = 0;
			state = "pick actions";
			pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
			return;

		case ("pick actions"):
			if (action_selected > 0) {
				continuer = true;
				if (action_selected == 1) {
					pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("attacks"));
					state = "select enemy";
					pending_choices.Add (new List<string> { "Who will " + good_guys[picker].name + " target?", bad_guys[0].name, bad_guys[1].name, bad_guys[2].name, ""});
				} else if (action_selected == 2) {
					state = "select ability";
					pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listAbilities ());
				} else if (action_selected == 3) {
					pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("guards"));
					state = "select teammate";
					pending_choices.Add (new List<string> { "Who will " + good_guys[picker].name + " target?", good_guys[0].name, good_guys[1].name, good_guys[2].name, good_guys[3].name});
				} else {
					state = "select item";
					pending_choices.Add (item_list);
				}
				action_selected = 0;
				return;
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
				pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions());
			}
			return;

		case ("select ability"):
			need_target = 'n';
			if (action_selected > 0) {
				pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("ability", action_selected));
				if (need_target == 'e') {
					state = "select enemy";
					pending_choices.Add (new List<string> { "Who will " + good_guys[picker].name + " target?", bad_guys[0].name, bad_guys[1].name, bad_guys[2].name, ""});
				} else if (need_target == 'a') {
					state = "select teammate";
					pending_choices.Add (new List<string> { "Who will " + good_guys[picker].name + " target?", good_guys[0].name, good_guys[1].name, good_guys[2].name, good_guys[3].name});
				} else {
					state = "pick actions";
					picker++;
					if (picker < good_guys.Count) {
						pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
					}
				}
				action_selected = 0;
			}
			return;

		case ("select item"):
			need_target = 'n';
			if (action_selected > 0) {
				pending_messages.Add (good_guys [picker].GetComponent<FightBehavior> ().setAction ("item", action_selected));
				if (need_target == 'e') {
					state = "select enemy";
					pending_choices.Add (new List<string> { "Who will " + good_guys[picker].name + " target?", bad_guys[0].name, bad_guys[1].name, bad_guys[2].name, ""});
				} else if (need_target == 'a') {
					state = "select teammate";
					pending_choices.Add (new List<string> { "Who will " + good_guys[picker].name + " target?", good_guys[0].name, good_guys[1].name, good_guys[2].name, good_guys[3].name});
				} else {
					state = "pick actions";
					picker++;
					if (picker < good_guys.Count) {
						pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
					}
				}
				action_selected = 0;
			}
			return;

		case ("select enemy"):
			if (action_selected > 0) {
				good_guys [picker].GetComponent<FightBehavior> ().setTarget (bad_guys [action_selected - 1]);
				pending_messages.Add (good_guys [picker].name + " will target " + bad_guys [action_selected - 1].name + "!");
				state = "pick actions";
				picker++;
				if (picker < good_guys.Count) {
					pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
				}
				action_selected = 0;
			}
			return;

		case ("select teammate"):
			if (action_selected > 0) {
				good_guys [picker].GetComponent<FightBehavior> ().setTarget (good_guys [action_selected - 1]);
				pending_messages.Add (good_guys [picker].name + " will target " + good_guys [action_selected - 1].name + "!");
				state = "pick actions";
				picker++;
				if (picker < good_guys.Count) {
					pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
				}
				action_selected = 0;
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
				picker = 0;
				pending_choices.Add (good_guys [picker].GetComponent<FightBehavior> ().listActions ());
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
		action_selected = int.Parse (button_name);
		awaiting_input = false;
	}
}
