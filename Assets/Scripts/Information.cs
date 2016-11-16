using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Information : MonoBehaviour {


	Dictionary<string, int> inventory;
	List<Party_Member> party;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		inventory = new Dictionary<string, int> ();
		defaultInventory ();

		party = new List<Party_Member> ();
		defaultParty ();
	}

	void defaultInventory(){
		inventory ["Potion"] = 3;
		inventory ["Panacea Bottle"] = 4;
		inventory ["Magic Lens"] = 5;
		inventory ["The Kevin-Beater Bat"] = 99;
		inventory ["The Orange Overlord"] = 270;
	}

	void defaultParty(){
		party.Add (new Party_Member ("Sam", 3, 10));
		party.Add (new Party_Member ("Amelia", 4, 10));
		party.Add (new Party_Member ("Cody", 10, 10));
		party.Add (new Party_Member ("Nico", 2, 10));
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
}
