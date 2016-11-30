using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Information : MonoBehaviour {


	Dictionary<string, int> inventory;
	List<Party_Member> party;

	//Overworld Save Data
	//Remember to wipe these when starting a new level
	Vector3 mainCharacterPosition = Vector3.zero;
	int progress_number = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		inventory = new Dictionary<string, int> ();
		defaultInventory ();

		party = new List<Party_Member> ();
		defaultParty ();

		if (progress_number != 0) {
			//we must be starting from a save point
			OverworldLoad();
		}
	}

	void defaultInventory(){
		inventory ["Potion"] = 3;
		inventory ["Panacea Bottle"] = 4;
		inventory ["Magic Lens"] = 5;
		inventory ["The Kevin-Beater Bat"] = 99;
		inventory ["The Orange Overlord"] = 270;
		inventory ["Life Bottle"] = 5;
	}

	void defaultParty(){
		party.Add (new Party_Member ("Sam", 60, 10, 12));
		party.Add (new Party_Member ("Amelia", 60, 10, 15));
		party.Add (new Party_Member ("Cody", 100, 10, 10));
		party.Add (new Party_Member ("Nico", 40, 10, 18));
	}

	public Party_Member getPartyMember(string name){
		foreach (Party_Member person in party) {
			if (person.name == name) {
				return person;
			}
		}
		Debug.Log ("Couldn't find the person: " + name);
		return null;
	}

	public void updatePartyMember(string name, int health, int mana){
		foreach (Party_Member person in party) {
			if (person.name == name) {
				person.health = health;
				person.mana = mana;
			}
		}
	}

	public void getItem(string item_name, int amount){
		inventory [item_name] += amount;
	}

	public void useItem(string item_name){
		inventory [item_name]--;
	}

	public List<string> getItemNames(){
		List<string> result = new List<string> ();
		foreach (string item in inventory.Keys) {
			result.Add (item);
		}
		return result;
	}

	public List<int> getItemAmounts(){
		List<int> result = new List<int> ();
		foreach (int item in inventory.Values) {
			result.Add (item);
		}
		return result;
	}

	public void addItemToInventory(string item, int number){
		if (inventory.ContainsKey (item)) {
			inventory [item] += number;
		} else {
			inventory.Add (item, number);
		}
		//TEST STUFF
		//printInventory ();
	}

	void printInventory(){
		foreach (string key in inventory.Keys) {
			print (key + ": " + inventory [key]);
		}
	}

	public void OverworldSave(){
		mainCharacterPosition = GameObject.FindObjectOfType<CharacterController> ().gameObject.transform.position;
		progress_number = GameObject.FindObjectOfType<ProgressLevel> ().getOverworldProgress();
	}

	public void OverworldLoad(){
		GameObject.FindObjectOfType<CharacterController> ().gameObject.transform.position = mainCharacterPosition;
		GameObject.FindObjectOfType<ProgressLevel> ().ProgressTo (progress_number);
	}
		
}
