using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

	List<FightBehavior> good_guys;
	List<FightBehavior> bad_guys;
	List<FightBehavior> participants;
	List<FightBehavior> dead;
	List<FightBehavior> defeated_enemies;
	List<string> item_list;
	List<int> item_list_amounts;
	string state;
	int picker;
	int picker2;
	bool awaiting_input;
	bool victory;
	bool defeat;
	bool continuer;
	public bool message_finished;
	char need_target;
	List<FightBehavior> pending_actions;
	List<string> pending_messages;
	List<List<string>> pending_choices;
	TextControl text_controller;
	ItemBehavior inventory;
	Information info;
	int action_selected;
	int turn_number;

	// Use this for initialization
	void Start () {
		info = FindObjectOfType<Information> ();
		state = "not started";
		good_guys = new List<FightBehavior> ();
		bad_guys = new List<FightBehavior> ();
		participants = new List<FightBehavior> ();
		dead = new List<FightBehavior> ();
		defeated_enemies = new List<FightBehavior> ();
		item_list = new List<string> { "Pick an item to use!" };
		item_list_amounts = new List<int>{ 0 };
		StartBattle ();
		awaiting_input = false;
		victory = false;
		defeat = false;
		continuer = false;
		need_target = 'n';
		pending_actions = new List<FightBehavior> ();
		pending_messages = new List<string> ();
		text_controller = GameObject.Find ("Text Controller").GetComponent<TextControl> ();
		pending_choices = new List<List<string>> ();
		inventory = GetComponent<ItemBehavior> ();
		SpriteRenderer backgroundo = GameObject.FindObjectOfType<FitBackgroundToCamera> ().gameObject.GetComponent<SpriteRenderer> ();
		backgroundo.sprite = Resources.Load<Sprite> (info.current_section);
	}

	public List<FightBehavior> getGoodGuys(){
		return good_guys;
	}

	public List<FightBehavior> getBadGuys(){
		return bad_guys;
	}

	public string getItemName(int which){
		return item_list [which];
	}

	public char itemNeedsTargeting(int which){
		return inventory.needsTargeting (item_list[which]);
	}

	public string useItem(int which, FightBehavior user, FightBehavior target){
		return inventory.useItem(item_list[which], user, target);
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

		//if (Input.GetKeyDown (KeyCode.P))
			//ParticleManager.doEffect ("bolt", bad_guys [0]);


		if (Input.GetKeyDown (KeyCode.Backspace)) {
			if (state == "pick actions") {
				if (picker > 0) {
					awaiting_input = false;
					if (good_guys [picker - 1].turn_action == "item") {
						item_list_amounts [good_guys [picker - 1].action_number]++;
					}
					picker--;
					pending_choices.Add (good_guys [picker].listActions ());
				}
			} else if (state.Split (' ') [0] == "select") {
				awaiting_input = false;
				state = "pick actions";
				continuer = false;
				pending_choices.Add (good_guys [picker].listActions ());
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
			pending_messages.AddRange(pending_actions [0].doAction ());
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
			pending_choices.Add (good_guys [picker].listActions ());
			return;

		case ("pick actions"):
			if (action_selected > 0) {
				continuer = true;
				if (action_selected == 1) {
					good_guys [picker].setAction ("attacks");
					state = "select enemy";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " attack?" };
					for (int x = 0; x < bad_guys.Count; x++) {
						result.Add (bad_guys [x].character_name);
					}
					pending_choices.Add (result);
				} else if (action_selected == 2) {
					state = "select ability";
					pending_choices.Add (good_guys [picker].listAbilities ());
				} else if (action_selected == 3) {
					good_guys [picker].setAction ("guards");
					state = "select teammate";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " guard?" };
					for (int x = 0; x < good_guys.Count; x++) {
						result.Add (good_guys [x].character_name);
					}
					pending_choices.Add (result);
				} else {
					state = "select item";
					List<string> current_items = new List<string> ();
					current_items.Add(item_list[0]);
					for (int x = 1; x < item_list.Count; x++) {
						current_items.Add (item_list [x] + " - " + item_list_amounts [x]);
					}
					pending_choices.Add (current_items);
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
				pending_choices.Add (good_guys [picker].listActions());
			}
			return;

		case ("select ability"):
			need_target = 'n';
			if (action_selected > 0) {
				if (!good_guys [picker].enoughMana (action_selected)) {
					pending_messages.Add (good_guys [picker].character_name + " doesn't have enough mana for that ability!");
					pending_choices.Add (good_guys [picker].listAbilities ());
					action_selected = 0;
					return;
				}
				pending_messages.Add (good_guys [picker].setAction ("ability", action_selected));
				if (need_target == 'e') {
					state = "select enemy";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " target?" };
					for (int x = 0; x < bad_guys.Count; x++) {
						result.Add (bad_guys [x].character_name);
					}
					pending_choices.Add (result);
				} else if (need_target == 'a') {
					state = "select teammate";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " target?" };
					for (int x = 0; x < good_guys.Count; x++) {
						result.Add (good_guys [x].character_name);
					}
					pending_choices.Add (result);
				} else if (need_target == 'd') {
					state = "select dead";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " target?" };
					for (int x = 0; x < dead.Count; x++) {
						result.Add (dead [x].character_name);
					}
					pending_choices.Add (result);
				} else {
					state = "pick actions";
					picker++;
					if (picker < good_guys.Count) {
						pending_choices.Add (good_guys [picker].listActions ());
					}
				}
				action_selected = 0;
			}
			return;

		case ("select item"):
			need_target = 'n';
			if (action_selected > 0) {
				if (item_list_amounts [action_selected] < 1) {
					action_selected = 0;
					return;
				}
				pending_messages.Add (good_guys [picker].setAction ("item", action_selected));
				item_list_amounts [action_selected]--;
				if (need_target == 'e') {
					state = "select enemy";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " target?" };
					for (int x = 0; x < bad_guys.Count; x++) {
						result.Add (bad_guys [x].character_name);
					}
					pending_choices.Add (result);
				} else if (need_target == 'a') {
					state = "select teammate";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " target?" };
					for (int x = 0; x < good_guys.Count; x++) {
						result.Add (good_guys [x].character_name);
					}
					pending_choices.Add (result);
				} else if (need_target == 'd') {
					state = "select dead";
					List<string> result = new List<string> { "Who will " + good_guys [picker].character_name + " target?" };
					for (int x = 0; x < dead.Count; x++) {
						result.Add (dead [x].character_name);
					}
					pending_choices.Add (result);
				} else {
					state = "pick actions";
					picker++;
					if (picker < good_guys.Count) {
						pending_choices.Add (good_guys [picker].listActions ());
					}
				}
				action_selected = 0;
			}
			return;

		case ("select enemy"):
			if (action_selected > 0) {
				good_guys [picker].setTarget (bad_guys [action_selected - 1]);
				pending_messages.Add (good_guys [picker].character_name + " will target " + bad_guys [action_selected - 1].character_name + "!");
				state = "pick actions";
				picker++;
				if (picker < good_guys.Count) {
					pending_choices.Add (good_guys [picker].listActions ());
				}
				action_selected = 0;
			}
			return;

		case ("select teammate"):
			if (action_selected > 0) {
				good_guys [picker].setTarget (good_guys [action_selected - 1]);
				pending_messages.Add (good_guys [picker].character_name + " will target " + good_guys [action_selected - 1].character_name + "!");
				state = "pick actions";
				picker++;
				if (picker < good_guys.Count) {
					pending_choices.Add (good_guys [picker].listActions ());
				}
				action_selected = 0;
			}
			return;

		case("select dead"):
			if (action_selected > 0) {
				good_guys [picker].setTarget (dead [action_selected - 1], true);
				pending_messages.Add (good_guys [picker].character_name + " will revive " + dead [action_selected - 1].character_name + "!");
				state = "pick actions";
				picker++;
				if (picker < good_guys.Count) {
					pending_choices.Add (good_guys [picker].listActions ());
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
				while (!participants [picker].gameObject.activeSelf) {
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
				while (!participants [picker].gameObject.activeSelf) {
					picker++;
					if (picker >= participants.Count) {
						return;
					}
				}
				pending_messages.AddRange (participants [picker].endTurn ());
				continuer = true;
			} else {
				turn_number++;
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
				foreach (FightBehavior participant in participants) {
					participant.sendInfoUpdate ();
				}
				state = "finished";
			} else {
				state = "pick actions";
				picker = 0;
				pending_choices.Add (good_guys [picker].listActions ());
			}
			return;

		case("finished"):
			SceneManager.LoadScene (info.GetOverworldName());
			return;

		default:
			return;
		}
	}

	void StartBattle(){
		List<string> good = info.getAllies ();
		List<string> bad = info.getEnemies ();
		for (int x = 0; x < good.Count; x++) {
			FightBehavior temp = ((GameObject)Instantiate (Resources.Load (good [x]), Vector3.zero, Quaternion.identity)).GetComponent<FightBehavior>();
			temp.setAlignment (true);
			good_guys.Add (temp);
			participants.Add (temp);
		}
		for (int x = 0; x < bad.Count; x++) {
			FightBehavior temp = ((GameObject)Instantiate (Resources.Load (bad [x]), Vector3.zero, Quaternion.identity)).GetComponent<FightBehavior>();
			temp.setAlignment (false);
			bad_guys.Add (temp);
			participants.Add (temp);
		}
		state = "instantiated";
		List<PositionCharactersInBattle> tempy = new List<PositionCharactersInBattle> ();
		tempy.AddRange (FindObjectsOfType<PositionCharactersInBattle> ());
		foreach (PositionCharactersInBattle item in tempy) {
			item.ArrangeCharacters (good_guys, bad_guys);
		}
	
		item_list.AddRange( info.getItemNames () );
		item_list_amounts.AddRange( info.getItemAmounts () );
		turn_number = 1;

	}

	public void kill(FightBehavior which){
		if (which.getAlignment ()) {
			good_guys.Remove (which);
			dead.Add (which);
		} else {
			bad_guys.Remove (which);
			defeated_enemies.Add (which);
		}
		
		if (good_guys.Count == 0) {
			defeat = true;
		} else if (bad_guys.Count == 0) {
			victory = true;
		}
	}

	public string revive(FightBehavior who){
		who.gameObject.SetActive (true);
		if (who.getAlignment ()) {
			dead.Remove (who);
			good_guys.Add (who);
			who.heal (who.getMaxHealth () / 2);
			return who.character_name + " has been revived!";
		} else {
			defeated_enemies.Remove (who);
			bad_guys.Add (who);
			who.heal (who.getMaxHealth ());
			return "Another " + who.character_name + " has arrived to the battlefield!";
		}


	}

	public List<string> reviveTeam(bool good_guys){
		List<string> result = new List<string> ();
		if (good_guys) {
			for (int x = 0; x < dead.Count; x++) {
				result.Add (revive (dead [x]));
			}
			return result;
		} else {
			for (int x = 0; x < defeated_enemies.Count; x++) {
				result.Add (revive (defeated_enemies [x]));
			}
			return result;
		}

	}
			

	public void newTarget(FightBehavior which, bool good){
		if (good) {
			which.setTarget(bad_guys [Random.Range (0, bad_guys.Count)]);
		} else {
			which.setTarget(good_guys [Random.Range (0, good_guys.Count)]);
		}
	}

	public void newTargetWeakest(FightBehavior which, bool good){
		if (good) {
			int weakest = bad_guys [0].getHealth ();
			which.setTarget (bad_guys [0]);
			for (int x = 1; x < bad_guys.Count; x++) {
				if (bad_guys [x].getHealth () < weakest) {
					weakest = bad_guys [x].getHealth ();
					which.setTarget (bad_guys [x]);
				}
			}
		} else {
			int weakest = good_guys [0].getHealth ();
			which.setTarget (good_guys [0]);
			for (int x = 1; x < good_guys.Count; x++) {
				if (good_guys [x].getHealth () < weakest) {
					weakest = good_guys [x].getHealth ();
					which.setTarget (good_guys [x]);
				}
			}
		}
	}

	public void ReceiveButtonSignal(string button_name){
		action_selected = int.Parse (button_name);
		awaiting_input = false;
	}

	public int getTurnNumber(){
		return turn_number;
	}
}
