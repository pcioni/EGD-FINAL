using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour {

	List<GameObject> good_guys;
	List<GameObject> bad_guys;
	List<GameObject> participants;
	string state;
	int picker;
	bool awaiting_input;
	bool victory;

	// Use this for initialization
	void Start () {
		state = "not started";
		good_guys = new List<GameObject> ();
		bad_guys = new List<GameObject> ();
		participants = new List<GameObject> ();
		StartBattle (new List<string>{ "Red", "Green", "Blue" }, new List<string>{ "Blue", "Blue", "Red", "Red" });
		awaiting_input = true;
		victory = false;
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {

		case ("not started"):
			return;

		case ("instantiated"):
			Debug.Log ("A battle has begun!");
			picker = 0;
			state = "pick actions";
			Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			return;

		case ("pick actions"):
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.G) || Input.GetKeyDown(KeyCode.V)) {
				if (Input.GetKeyDown (KeyCode.A)) {
					Debug.Log (good_guys [picker].name + " will attack this turn!");
					good_guys [picker].GetComponent<FightBehavior> ().setAction ("attacks");
				} else if (Input.GetKeyDown (KeyCode.G)) {
					Debug.Log (good_guys [picker].name + " will guard this turn!");
					good_guys [picker].GetComponent<FightBehavior> ().setAction ("guards themself");
				} else if (Input.GetKeyDown (KeyCode.V)) {
					Debug.Log (good_guys [picker].name + " has invoked the win condition!");
					good_guys [picker].GetComponent<FightBehavior> ().setAction ("defeats all the enemies");
				}
				picker++;
				if (picker >= good_guys.Count) {
					Debug.Log ("All actions have been selected! Press space to fight!");
					state = "commence";
					picker = 0;
					awaiting_input = true;
					return;
				}
				Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
			}
			return;

		case ("commence"):
			if (Input.GetKeyDown (KeyCode.Space)) {
				awaiting_input = false;
			}
			if (!awaiting_input) {
				if (picker < participants.Count) {
					participants [picker].GetComponent<FightBehavior> ().doAction ();
					if (victory) {
						picker = participants.Count;
					}
					picker++;
					awaiting_input = true;
				} 
				else if (picker == participants.Count) {
					picker++;
				} 
				else {
					picker = 0;
					state = "check win";
					Debug.Log ("The turn has ended. Press space to continue.");
				}
			}
			return;

		case ("check win"):
			if (victory) {
				Debug.Log ("Congratulations, you have won!");
				state = "";
			} else {
				if (Input.GetKeyDown (KeyCode.Space)) {
					state = "pick actions";
					Debug.Log ("Pick an action for " + good_guys [picker].name + ": A to attack or G to guard!");
				}
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
			good_guys.Add (temp);
			participants.Add (temp);
			positiony += new Vector3 (0f, -1f, 0f);
		}
		positiony = new Vector3 (2f, 2f, 0f);
		for (int x = 0; x < bad.Count; x++) {
			GameObject temp = (GameObject)Instantiate (Resources.Load (bad [x]), positiony, Quaternion.identity);
			bad_guys.Add (temp);
			participants.Add (temp);
			positiony += new Vector3 (0f, -1f, 0f);
		}
		state = "instantiated";
	}

	public void KillEnemies(){
		for (int x = 0; x < bad_guys.Count; x++) {
			Destroy (bad_guys [x]);
			victory = true;
		}
	}
}
