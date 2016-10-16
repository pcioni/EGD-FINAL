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
	bool continuer;
	bool await_result;
	char need_target;
	string pending_message;

	// Use this for initialization
	void Start () {
		state = "not started";
		good_guys = new List<GameObject> ();
		bad_guys = new List<GameObject> ();
		participants = new List<GameObject> ();
		StartBattle (new List<string>{ "Red", "Green", "Blue" }, new List<string>{ "Blue", "Blue", "Red", "Red" });
		awaiting_input = false;
		victory = false;
		continuer = false;
		await_result = false;
		need_target = 'n';
	}

	public void SendMessagey(string message){
		Debug.Log (message);
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
				Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				if (picker > 0) {
					picker--;
					Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
				}
			}
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.G) || Input.GetKeyDown(KeyCode.V)) {
				continuer = true;
				if (Input.GetKeyDown (KeyCode.A)) {
					Debug.Log (good_guys [picker].GetComponent<FightBehavior> ().setAction ("attacks"));
				} else if (Input.GetKeyDown (KeyCode.G)) {
					Debug.Log (good_guys [picker].GetComponent<FightBehavior> ().setAction ("guards"));
				} else if (Input.GetKeyDown (KeyCode.V)) {
					SendMessagey (good_guys [picker].GetComponent<FightBehavior> ().setAction ("insta-kill"));
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
				Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			return;

		case ("select enemy"):
			if (Input.GetKeyDown (KeyCode.Backspace)) {
				state = "pick actions";
				continuer = false;
				Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (picker2 == -1) {
				picker2++;
				Debug.Log ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
			} else {
				if (Input.GetKeyDown (KeyCode.W)) {
					picker2--;
					if (picker2 < 0) {
						picker2 = bad_guys.Count - 1;
					}
					Debug.Log ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.S)) {
					picker2++;
					if (picker2 >= bad_guys.Count) {
						picker2 = 0;
					}
					Debug.Log ("Who will " + good_guys[picker].name + " target? Currently targeting: " + bad_guys [picker2].name);
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
				Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			if (picker2 == -1) {
				picker2++;
				Debug.Log ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
			} else {
				if (Input.GetKeyDown (KeyCode.W)) {
					picker2--;
					if (picker2 < 0) {
						picker2 = good_guys.Count - 1;
					}
					Debug.Log ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.S)) {
					picker2++;
					if (picker2 >= good_guys.Count) {
						picker2 = 0;
					}
					Debug.Log ("Who will " + good_guys[picker].name + " target? Currently targeting: " + good_guys [picker2].name);
				} else if (Input.GetKeyDown (KeyCode.Space)) {
					SendMessagey (good_guys [picker].name + " will target " + good_guys [picker2].name  + "!");
					good_guys [picker].GetComponent<FightBehavior> ().setTarget (good_guys [picker2]);
					state = "pick actions";
				}
			}
			return;

		case ("commence"):
			if (await_result) {
				sendPending ();
				await_result = false;
				continuer = true;
				return;
			}
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
				SendMessagey (participants [picker].GetComponent<FightBehavior> ().doAction ());
				await_result = true;
			}
			else {
				picker = 0;
				state = "check win";
				SendMessagey ("The turn has ended. Press space to continue.");
			}
			return;

		case ("check win"):
			if (victory) {
				Debug.Log ("Congratulations, you have won!");
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

	public void KillEnemies(){
		for (int x = 0; x < bad_guys.Count; x++) {
			bad_guys [x].SetActive (false);
			bad_guys.Remove (bad_guys [x]);
			victory = true;
		}
	}

	public void kill(GameObject which){
		bad_guys.Remove (which);
		good_guys.Remove (which);
		which.SetActive (false);
		if (bad_guys.Count == 0) {
			victory = true;
		}
	}

	public void setPending(string message){
		pending_message = message;
	}

	public void sendPending(){
		if (pending_message != "") {
			SendMessagey (pending_message);
			pending_message = "";
		}
	}

	public void newTarget(GameObject which, bool good){
		if (good) {
			which.GetComponent<FightBehavior> ().setTarget(bad_guys [0]);
		} else {
			which.GetComponent<FightBehavior> ().setTarget(good_guys [0]);
		}
	}
}
