using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Information : MonoBehaviour {


	Dictionary<string, int> inventory;
	List<Party_Member> party;
	List<string> good_guys;
	List<string> bad_guys;
	List<string> intro_dialogue;
	List<string> exit_dialogue;
	Dictionary<int, string> in_battle_events;

	public int current_level = 3;

	//Overworld Save Data
	//Remember to wipe these when starting a new level
	Vector3 mainCharacterPosition = Vector3.zero;
	public int progress_number = 0;
	public string scene_name = "";
	public string current_section = "";
	public string disable_battle = "";
	public int dialogue_index = -1;
	public Dictionary<string, int> talked_to;
	//the string is someone not to fight again,
	//the int is which dialogue index you left off on

	public string song;
	AudioSource audio_source;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
		inventory = new Dictionary<string, int> ();
		defaultInventory ();

		party = new List<Party_Member> ();
		defaultParty ();

		good_guys = new List<string> ();
		bad_guys = new List<string> ();

		talked_to = new Dictionary<string, int> ();

		audio_source = GameObject.FindObjectOfType<AudioSource> ();
	}

	void defaultInventory(){
		inventory ["Potion"] = 3;
		inventory ["Panacea Bottle"] = 4;
		inventory ["Magic Lens"] = 100;
		inventory ["Blast Powder"] = 2;
		inventory ["Flash Powder"] = 2;
		inventory ["Life Bottle"] = 1;
		inventory ["Incense"] = 3;
	}

	void defaultParty(){
		party.Add (new Party_Member ("Sam", 70, 10, 12));
		party.Add (new Party_Member ("Amelia", 60, 10, 15));
		party.Add (new Party_Member ("Cody", 100, 10, 10));
		party.Add (new Party_Member ("Nico", 40, 10, 18));
		party.Add (new Party_Member ("Amanda", 50, 10, 16));
		party.Add (new Party_Member ("Bully", 90, 10, 13));
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

	public void restoreParty(){
		for (int x = 0; x < party.Count; x++) {
			party [x].health = party [x].max_health;
			party [x].mana = party [x].max_mana;
		}
	}

	void printInventory(){
		foreach (string key in inventory.Keys) {
			print (key + ": " + inventory [key]);
		}
	}

	public void OverworldSave(/*string caller = "", int dialogue_position = -1*/){
		mainCharacterPosition = GameObject.FindObjectOfType<CharacterController> ().gameObject.transform.position;
		progress_number = GameObject.FindObjectOfType<ProgressLevel> ().getOverworldProgress();
		scene_name = SceneManager.GetActiveScene().name;

		foreach (Camera c in GameObject.FindObjectsOfType<Camera>()) {
			if (c.enabled) {
				current_section = c.transform.parent.gameObject.name;
				//print ("first scene will be " + current_section);
			}
		}
		/*
		disable_battle = caller;
		if (dialogue_position != -1) dialogue_index = dialogue_position;
		*/


	}

	public void OverworldLoad(){
		GameObject.FindObjectOfType<CharacterController> ().gameObject.transform.position = mainCharacterPosition;
		GameObject.FindObjectOfType<ProgressLevel> ().ProgressTo (progress_number);
		foreach (IdentifyFirstScene i in GameObject.FindObjectsOfType<IdentifyFirstScene>()) {
			if (i.name != current_section) {
				i.firstScene = false;
			} else {
				i.firstScene = true;
			}
		}
		/*if (disable_battle != "" || dialogue_index != -1){
			InteractableSpeaker s = GameObject.Find (disable_battle).GetComponent<InteractableSpeaker> ();
			print (s.name + "will no longer be able to battle!");
			if (disable_battle != "") {
				s.our_team = new string[0];
				s.enemy_team = new string[0];
			}
			if (dialogue_index != -1) {
				s.dialogueIndex = dialogue_index;
			}
		}*/
			
	}

	public void setBattlers(string[] goods, string[] bads){
		good_guys.Clear ();
		bad_guys.Clear ();
		good_guys.AddRange(goods);
		bad_guys.AddRange(bads);
	}

	public List<string> getAllies(){
		return good_guys;
	}

	public List<string> getEnemies(){
		//oh no you have enemies now!
		return bad_guys;
	}

	public string GetOverworldName(){
		return scene_name;
	}

	public void IncrementLevelNumber(){
		current_level++;
	}

	public int GetLevelNumber(){
		return current_level;
	}

	public void setIntroDialogue(List<string> words){
		intro_dialogue = words;
	}
		
	public List<string> getIntroDialogue(){
		return intro_dialogue;
	}

	public bool introDialogue(){
		if (intro_dialogue != null) {
			return intro_dialogue.Count > 0;
		}
		return false;
	}

	public void setExitDialogue(List<string> words){
		exit_dialogue = words;
	}

	public List<string> getExitDialogue(){
		return exit_dialogue;
	}

	public bool exitDialogue(){
		if (exit_dialogue != null) {
			return exit_dialogue.Count > 0;
		}
		return false;
	}

	public void setBattleEvents(Dictionary<int, string> events){
		in_battle_events = events;
	}

	public Dictionary<int, string> getBattleEvents(){
		return in_battle_events;
	}

	public bool battleEvents(){
		if (in_battle_events != null) {
			return in_battle_events.Count > 0;
		}
		return false;
	}

	public void startSong(){
		if (song == "Boss")
			audio_source.clip = Resources.Load ("Boss");
		else if (song == "Dragon")
			audio_source.clip = Resources.Load ("Dragon");
		audio_source.Play ();
	}
}
